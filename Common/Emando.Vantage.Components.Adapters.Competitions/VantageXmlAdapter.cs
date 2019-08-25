using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Adapters.Competitions.Properties;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Components.Competitions.Sync;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Adapters.Competitions
{
    [Adapter("Vantage XML", 10)]
    public class VantageXmlAdapter : ICompetitionExportAdapter, ICompetitionImportAdapter, ICompetitionResultsImportAdapter
    {
        private readonly Func<ICompetitionContext> contextFactory;

        public VantageXmlAdapter(Func<ICompetitionContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        #region ICompetitionExportAdapter Members

        public string FileExtension => ".xml";

        public string MediaType => "text/xml";

        public async Task ExportAsync(Guid competitionId, Stream stream, CultureInfo culture)
        {
            var data = new VantageSyncData();
            using (var context = contextFactory())
            {
                data.Competition = await context.Competitions
                    .Include(c => c.ReportTemplate.Logos)
                    .Include(c => c.CompetitorLists.Select(cl => cl.Competitors.Select(co => co.DistanceCombinations)))
                    .Include(c => c.Distances.Select(d => d.Races.Select(r => r.Results)))
                    .Include(c => c.Distances.Select(d => d.Races.Select(r => r.Times)))
                    .Include(c => c.Distances.Select(d => d.Races.Select(r => r.Laps)))
                    .Include(c => c.Distances.Select(d => d.Races.Select(r => r.Passings)))
                    .Include(c => c.Distances.Select(d => d.DistancePointsTable.Points))
                    .Include(c => c.DistanceCombinations.Select(dc => dc.Distances))
                    .FirstOrDefaultAsync(c => c.Id == competitionId);
                await context.TeamCompetitorMembers
                    .Include(tcm => tcm.Member)
                    .Where(tcm => tcm.Team.List.CompetitionId == competitionId).LoadAsync();

                var projection = await context.Competitors.OfType<PersonCompetitor>()
                    .Where(p => p.List.CompetitionId == competitionId)
                    .Select(p => new
                    {
                        p.Person,
                        Licenses = p.Person.Licenses.Where(l => p.List.Competition.Discipline.StartsWith(l.Discipline))
                    })
                    .ToListAsync();

                data.People = projection.Select(p => p.Person).ToList();
            }

            VantageXml.Save(data, stream);
        }

        #endregion

        #region ICompetitionImportAdapter Members

        public async Task ImportAsync(string name, Stream stream, bool importPeople = true, Func<Competition, int> overrideClass = null)
        {
            var data = VantageXml.Load(stream);
            var competition = data.Competition;
            if (competition != null && overrideClass != null)
                competition.Class = overrideClass(competition);

            using (var context = contextFactory())
            using (var transaction = context.BeginTransaction(IsolationLevel.Serializable))
                try
                {
                    if (data.People != null && importPeople)
                        await ImportPeopleAsync(context, data.People);

                    if (competition != null)
                    {
                        var current = await context.Competitions.FirstOrDefaultAsync(c => c.Id == competition.Id);
                        if (current != null)
                            await ImportCompetitionResultsAsync(context, current, competition);
                        else
                        {
                            await ImportReportTemplateAsync(context, competition);
                            context.Competitions.Add(competition);
                            await context.SaveChangesAsync();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        private static async Task ImportReportTemplateAsync(IVantageContext context, Competition competition)
        {
            if (competition.ReportTemplate != null)
            {
                var reportTemplate = await context.ReportTemplates
                    .FirstOrDefaultAsync(t => t.LicenseIssuerId == competition.LicenseIssuerId && t.Name == competition.ReportTemplate.Name);
                if (reportTemplate == null)
                    context.ReportTemplates.Add(competition.ReportTemplate);
                else
                    competition.ReportTemplate = reportTemplate;
            }
        }

        private static async Task ImportPeopleAsync(IVantageContext context, ICollection<Person> people)
        {
            var identifiers = people.Select(p => p.Id);
            var existingPeople = await (from p in context.Persons.Include(p => p.Licenses)
                                        where identifiers.Contains(p.Id)
                                        select p).ToListAsync();

            foreach (var item in from np in people
                                 join ep in existingPeople on np.Id equals ep.Id into g
                                 from ep in g.DefaultIfEmpty()
                                 select new
                                 {
                                     New = np,
                                     Existing = ep
                                 })
            {
                if (item.Existing == null)
                    context.Persons.Add(item.New);
                else
                {
                    Mapper.Map(item.New, item.Existing);
                    foreach (var license in from nl in item.New.Licenses
                                            join el in item.Existing.Licenses on new
                                            {
                                                nl.IssuerId,
                                                nl.Discipline,
                                                nl.Key
                                            } equals new
                                            {
                                                el.IssuerId,
                                                el.Discipline,
                                                el.Key
                                            } into g
                                            from el in g.DefaultIfEmpty()
                                            select new
                                            {
                                                New = nl,
                                                Existing = el
                                            })
                    {
                        if (license.Existing == null)
                            context.PersonLicenses.Add(license.New);
                        else
                            Mapper.Map(license.New, license.Existing);
                    }
                }
            }
            await context.SaveChangesAsync();
        }

        #endregion

        #region ICompetitionResultsImportAdapter Members

        public async Task ImportAsync(Guid competitionId, string name, Stream stream, CultureInfo cultureInfo)
        {
            var data = VantageXml.Load(stream);

            using (var context = contextFactory())
            using (var transaction = context.BeginTransaction(IsolationLevel.Serializable))
                try
                {
                    if (data.Competition != null)
                    {
                        var competition = await context.Competitions.FirstOrDefaultAsync(c => c.Id == competitionId);
                        if (competition == null)
                            throw new CompetitionNotFoundException();

                        if (data.Competition.Id != competitionId)
                            throw new VantageImportException(Resources.CompetitionIdDoesNotMatch);

                        await ImportCompetitionResultsAsync(context, competition, data.Competition);
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        #endregion

        private static async Task ImportCompetitionResultsAsync(ICompetitionContext context, Competition current, Competition competition)
        {
            await ImportReportTemplateAsync(context, competition);
            current.ReportTemplate = competition.ReportTemplate;
            current.ResultsStatus = CompetitionResultsStatus.Unofficial;

            var existingCombinations = await context.DistanceCombinations.Include(dc => dc.Distances).Where(c => c.CompetitionId == competition.Id).ToListAsync();
            var existingDistances = await context.Distances.Include(d => d.Combinations).Where(c => c.CompetitionId == competition.Id).ToListAsync();
            var existingCompetitorLists = await context.CompetitorLists.Include(cl => cl.Competitors.Select(c => c.DistanceCombinations)).Where(r => r.CompetitionId == competition.Id).ToListAsync();

            foreach (var distance in existingDistances.Where(ed => competition.Distances.Any(nd => ed.Id == nd.Id || ed.Number == nd.Number)))
            {
                distance.Combinations.Clear();
                await context.SaveChangesAsync();
                context.Distances.Remove(distance);
                await context.SaveChangesAsync();
            }

            var competitorCombinations = new List<DistanceCombinationCompetitor>();
            foreach (var list in from ncl in competition.CompetitorLists
                                   join ecl in existingCompetitorLists on ncl.Id equals ecl.Id into g
                                   from ecl in g.DefaultIfEmpty()
                                   select new
                                   {
                                       New = ncl,
                                       Existing = ecl
                                   })
            {
                if (list.Existing == null)
                {
                    foreach (var competitor in list.New.Competitors)
                    {
                        competitorCombinations.AddRange(competitor.DistanceCombinations);
                        competitor.List = null;
                        competitor.Races = null;
                        competitor.DistanceCombinations = null;
                    }
                    context.CompetitorLists.Add(list.New);
                }
                else
                {
                    Mapper.Map(list.New, list.Existing);
                    await context.SaveChangesAsync();

                    foreach (var competitor in (from ec in list.Existing.Competitors
                                                join nc in list.New.Competitors on ec.Id equals nc.Id into g
                                                where !g.Any()
                                                select ec).ToList())
                    {
                        context.Competitors.Remove(competitor);
                        await context.SaveChangesAsync();
                    }

                    var competitors = (from nc in list.New.Competitors
                                       join ec in list.Existing.Competitors on nc.Id equals ec.Id into g
                                       from ec in g.DefaultIfEmpty()
                                       select new
                                       {
                                           New = nc,
                                           Existing = ec
                                       }).ToList();

                    foreach (var competitor in competitors.Where(c => c.Existing != null && c.Existing.StartNumber != c.New.StartNumber))
                    {
                        competitor.Existing.StartNumber += int.MaxValue / 2;
                        await context.SaveChangesAsync();
                    }

                    foreach (var competitor in competitors)
                    {
                        if (competitor.Existing == null)
                        {
                            competitorCombinations.AddRange(competitor.New.DistanceCombinations);
                            competitor.New.List = null;
                            competitor.New.Races = null;
                            competitor.New.DistanceCombinations = null;
                            context.Competitors.Add(competitor.New);
                        }
                        else
                        {
                            competitorCombinations.AddRange(competitor.New.DistanceCombinations);
                            Mapper.Map(competitor.New, competitor.Existing);
                        }
                        await context.SaveChangesAsync();
                    }
                }
                await context.SaveChangesAsync();
            }

            foreach (var distance in competition.Distances)
            {
                foreach (var race in distance.Races)
                    race.Competitor = null;

                if (distance.DistancePointsTable != null)
                {
                    var pointsTable = await context.DistancePointsTables.FirstOrDefaultAsync(t => t.Id == distance.DistancePointsTable.Id);
                    if (pointsTable == null)
                        context.DistancePointsTables.Add(distance.DistancePointsTable);
                    else
                        distance.DistancePointsTable = pointsTable;
                }

                context.Distances.Add(distance);
            }
            await context.SaveChangesAsync();

            foreach (var combination in (from edc in existingCombinations
                                         join ndc in competition.DistanceCombinations on edc.Id equals ndc.Id into g
                                         where !g.Any()
                                         select edc).ToList())
            {
                context.DistanceCombinations.Remove(combination);
                await context.SaveChangesAsync();
            }

            var combinations = (from ndc in competition.DistanceCombinations
                                orderby ndc.Number descending
                                join edc in existingCombinations on ndc.Id equals edc.Id into g
                                from edc in g.DefaultIfEmpty()
                                select new
                                {
                                    New = ndc,
                                    Existing = edc
                                }).ToList();

            foreach (var combination in combinations.Where(c => c.Existing != null && c.Existing.Number != c.New.Number))
            {
                combination.Existing.Number += int.MaxValue / 2;
                await context.SaveChangesAsync();
            }

            foreach (var cc in competitorCombinations)
            {
                cc.DistanceCombination = null;
                cc.Competitor = null;
            }

            foreach (var combination in combinations)
            {
                if (combination.Existing == null)
                {
                    combination.New.Competitors = competitorCombinations.Where(cc => cc.DistanceCombinationId == combination.New.Id).ToList();
                    context.DistanceCombinations.Add(combination.New);
                }
                else
                {
                    Mapper.Map(combination.New, combination.Existing);
                    combination.Existing.Competitors = competitorCombinations.Where(cc => cc.DistanceCombinationId == combination.Existing.Id).ToList();
                    combination.Existing.Distances = competition.Distances.Where(nd => combination.New.Distances.Any(ed => ed.Id == nd.Id)).ToList();
                }
                await context.SaveChangesAsync();
            }
        }
    }
}