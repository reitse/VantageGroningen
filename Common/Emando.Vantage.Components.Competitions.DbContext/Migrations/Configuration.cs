using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CompetitionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            CommandTimeout = 300;
        }

        protected override void Seed(CompetitionContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

#if TESTDATA
            context.Venues.AddOrUpdate(v => new
            {
                v.Discipline,
                v.Code
            },
                new Venue
                {
                    Name = "Test Venue",
                    Code = "XXX",
                    Discipline = "SpeedSkating.LongTrack",
                    Address =
                    {
                        City = "Test City",
                        CountryCode = "NED"
                    },
                    Tracks = new Collection<VenueTrack>
                    {
                        new VenueTrack
                        {
                            Length = 400
                        }
                    }
                });

            context.LicenseIssuers.AddOrUpdate(l => l.Id, new LicenseIssuer
            {
                Id = "EMANDO",
                Name = "Emando B.V.",
                ForwardUri = "http://emando.nl/forward/{0}/{1}"
            });

            var persons = new[]
            {
                new Person
                {
                    Id = new Guid("{7DA73CB4-75B6-4678-B2A1-0345E12C2164}"),
                    Gender = Gender.Male,
                    NationalityCode = "NED",
                    Name = new Name("S.", "Sven", "Kramer"),
                    BirthDate = new DateTime(1986, 1, 1),
                    Licenses = new Collection<PersonLicense>
                    {
                        new PersonLicense
                        {
                            Category = "HSA",
                            Class = null,
                            Discipline = "SpeedSkating.LongTrack",
                            IssuerId = "EMANDO",
                            Key = "7DA73CB4",
                            Season = 2014,
                            ValidFrom = new DateTime(2014, 1, 1),
                            ValidTo = new DateTime(2018, 12, 31)
                        }
                    }
                },
                new Person
                {
                    Id = new Guid("{63BBEE2F-4A74-4509-B7AF-2178B16624C1}"),
                    Gender = Gender.Male,
                    NationalityCode = "NED",
                    Name = new Name("K.", "Koen", "Verweij"),
                    BirthDate = new DateTime(1987, 1, 1),
                    Licenses = new Collection<PersonLicense>
                    {
                        new PersonLicense
                        {
                            Category = "HSB",
                            Class = 0,
                            Discipline = "SpeedSkating.LongTrack",
                            IssuerId = "EMANDO",
                            Key = "63BBEE2F",
                            Season = 2014,
                            ValidFrom = new DateTime(2014, 1, 1),
                            ValidTo = new DateTime(2018, 12, 31)
                        }
                    }
                },
                new Person
                {
                    Id = new Guid("{E1DD3357-52C8-4DB8-B4B0-7E4E6FD372BF}"),
                    Gender = Gender.Male,
                    NationalityCode = "NED",
                    BirthDate = new DateTime(1988, 1, 1),
                    Name = new Name("J.W.", "Jan", "Blokhuijsen"),
                    Licenses = new Collection<PersonLicense>
                    {
                        new PersonLicense
                        {
                            Category = "HSA",
                            Class = 0,
                            Discipline = "SpeedSkating.LongTrack",
                            IssuerId = "EMANDO",
                            Key = "E1DD3357",
                            Season = 2014,
                            ValidFrom = new DateTime(2014, 1, 1),
                            ValidTo = new DateTime(2018, 12, 31)
                        }
                    }
                },
                new Person
                {
                    Id = new Guid("{78F96F86-D557-4E8D-9BE6-DFA0D0418D51}"),
                    Gender = Gender.Male,
                    NationalityCode = "NED",
                    Name = new Name("W.", "Wouter", "olde", "Heuvel"),
                    BirthDate = new DateTime(1989, 1, 1),
                    Licenses = new Collection<PersonLicense>
                    {
                        new PersonLicense
                        {
                            Category = "HN1",
                            Class = 0,
                            Discipline = "SpeedSkating.LongTrack",
                            IssuerId = "EMANDO",
                            Key = "78F96F86",
                            Season = 2014,
                            ValidFrom = new DateTime(2014, 1, 1),
                            ValidTo = new DateTime(2018, 12, 31)
                        }
                    }
                }
            };

            var competition = new Competition
            {
                Id = new Guid("{838D233F-DDA5-45AE-AB65-C40A729BB7EE}"),
                Discipline = "SpeedSkating.LongTrack",
                Name = "NK Afstanden",
                Sponsor = "Emando",
                VenueCode = "XXX",
                Starts = DateTime.Today.AddHours(14).ToUniversalTime(),
                Ends = DateTime.Today.AddDays(2).AddHours(18).ToUniversalTime(),
                LicenseIssuerId = "EMANDO",
                Class = 0,
                Culture = "nl-NL",
                DistancePointsTables = new Collection<DistancePointsTable>
                {
                    new DistancePointsTable
                    {
                        Id = new Guid("{7B1EC997-83AD-4CA6-A47B-61DE3AEF5DB3}"),
                        Name = "Mass Start",
                        Points = new Collection<DistancePoints>
                        {
                            new DistancePoints
                            {
                                Ranking = 1,
                                Type = "Sprint",
                                Points = 5
                            },
                            new DistancePoints
                            {
                                Ranking = 2,
                                Type = "Sprint",
                                Points = 3
                            },
                            new DistancePoints
                            {
                                Ranking = 3,
                                Type = "Sprint",
                                Points = 2
                            },
                            new DistancePoints
                            {
                                Ranking = 4,
                                Type = "Sprint",
                                Points = 1
                            },
                            new DistancePoints
                            {
                                Ranking = 1,
                                Type = "Finish",
                                Points = 31
                            },
                            new DistancePoints
                            {
                                Ranking = 2,
                                Type = "Finish",
                                Points = 15
                            },
                            new DistancePoints
                            {
                                Ranking = 3,
                                Type = "Finish",
                                Points = 10
                            },
                            new DistancePoints
                            {
                                Ranking = 4,
                                Type = "Finish",
                                Points = 5
                            },
                            new DistancePoints
                            {
                                Ranking = 5,
                                Type = "Finish",
                                Points = 3
                            },
                            new DistancePoints
                            {
                                Ranking = 6,
                                Type = "Finish",
                                Points = 1
                            }
                        }
                    }
                }
            };
            var competitors = new List<CompetitorBase>
            {
                new PersonCompetitor
                {
                    Id = new Guid("{4D64037B-780E-4B75-A562-76BE7407F11A}"),
                    EntityId = persons[0].Id,
                    Name = persons[0].Name,
                    PersonId = persons[0].Id,
                    NationalityCode = "NED",
                    StartNumber = 1,
                    LicenseKey = "7DA73CB4"
                },
                new PersonCompetitor
                {
                    Id = new Guid("{A78B1F4C-A2F2-4F22-8B53-512998E048B1}"),
                    EntityId = persons[1].Id,
                    Name = persons[1].Name,
                    PersonId = persons[1].Id,
                    NationalityCode = "NED",
                    StartNumber = 2,
                    LicenseKey = "63BBEE2F"
                },
                new PersonCompetitor
                {
                    Id = new Guid("{447D15B4-CE07-4A20-A90D-9454F3C3EF26}"),
                    EntityId = persons[2].Id,
                    Name = persons[2].Name,
                    PersonId = persons[2].Id,
                    NationalityCode = "NED",
                    StartNumber = 3,
                    LicenseKey = "E1DD3357"
                },
                new PersonCompetitor
                {
                    Id = new Guid("{83872C9B-67D6-43C8-B71C-179E692C970E}"),
                    EntityId = persons[3].Id,
                    Name = persons[3].Name,
                    PersonId = persons[3].Id,
                    NationalityCode = "NED",
                    StartNumber = 4,
                    LicenseKey = "78F96F86"
                }
            };
            var competitorList = new PersonCompetitorList
            {
                Id = new Guid("{EA65ECEA-3F88-43D9-8B75-3679D5CDB1AB}"),
                CompetitionId = new Guid("{838D233F-DDA5-45AE-AB65-C40A729BB7EE}"),
                Name = "Men",
                Competitors = competitors.ToList()
            };
            var distances = new[]
            {
                new Distance
                {
                    Id = new Guid("{EFAF0485-CAB1-4530-8DAB-7434C675B491}"),
                    CompetitionId = competition.Id,
                    Name = "Men 500 meter",
                    TrackLength = 400,
                    Value = 500,
                    ValueQuantity = DistanceValueQuantity.Length,
                    ClassificationWeight = 500,
                    Number = 1,
                    Discipline = "SpeedSkating.LongTrack.PairsDistance.Individual",
                    StartMode = DistanceStartMode.SingleHeat,
                    Starts = competition.Starts
                },
                new Distance
                {
                    Id = new Guid("{803A201D-DD2D-4EFE-8D7E-9B79810F8B7D}"),
                    CompetitionId = competition.Id,
                    Name = "Men 10000 meter",
                    TrackLength = 400,
                    Value = 10000,
                    ValueQuantity = DistanceValueQuantity.Length,
                    ClassificationWeight = 500,
                    Number = 2,
                    Discipline = "SpeedSkating.LongTrack.PairsDistance.Individual",
                    StartMode = DistanceStartMode.MultipleHeats,
                    Starts = competition.Starts.AddHours(2)
                },
                new Distance
                {
                    Id = new Guid("{CDA81562-C3CF-47D5-95FE-BA07E736F12E}"),
                    CompetitionId = competition.Id,
                    PointsTableId = new Guid("{7B1EC997-83AD-4CA6-A47B-61DE3AEF5DB3}"),
                    Name = "Mass Start Men",
                    TrackLength = 400,
                    Value = 16,
                    ValueQuantity = DistanceValueQuantity.Count,
                    Number = 3,
                    Discipline = "SpeedSkating.LongTrack.MassStartDistance",
                    StartMode = DistanceStartMode.MultipleHeats,
                    Starts = competition.Starts.AddHours(4)
                }
            };

            context.Persons.AddOrUpdate(p => p.Id, persons);
            context.Competitions.AddOrUpdate(c => c.Id, competition);
            context.CompetitorLists.AddOrUpdate(c => c.Id, competitorList);
            context.Distances.AddOrUpdate(d => d.Id, distances);

            var distanceCombinations = new[]
            {
                new DistanceCombination
                {
                    Id = new Guid("{8B57CEEF-E525-4C6E-92FB-EC25DEAB05F6}"),
                    CompetitionId = new Guid("{838D233F-DDA5-45AE-AB65-C40A729BB7EE}"),
                    Number = 1,
                    CategoryFilter = "H*",
                    Class = 0,
                    Name = "Heren",
                    Distances = distances.ToList(),
                    Competitors = new Collection<DistanceCombinationCompetitor>
                    {
                        new DistanceCombinationCompetitor
                        {
                            CompetitorId = competitors[0].Id
                        },
                        new DistanceCombinationCompetitor
                        {
                            CompetitorId = competitors[1].Id
                        },
                        new DistanceCombinationCompetitor
                        {
                            CompetitorId = competitors[2].Id
                        },
                        new DistanceCombinationCompetitor
                        {
                            CompetitorId = competitors[3].Id,
                            Reserve = 1
                        }
                    }
                }
            };
            var races = new[]
            {
                new Race
                {
                    Id = new Guid("{E49A6646-F9BF-4605-84E0-E4BB15D4A582}"),
                    DistanceId = distances[0].Id,
                    CompetitorId = competitors[0].Id,
                    Heat = 1,
                    Lane = 0,
                    Color = 0,
                    PresentedInstanceName = "Primary",
                    Results = new Collection<RaceResult>
                    {
                        new RaceResult
                        {
                            InstanceName = "Primary",
                            State = RaceState.Done
                        },
                        new RaceResult
                        {
                            InstanceName = "Secondary",
                            State = RaceState.Done
                        }
                    },
                    Times = new Collection<RaceTime>
                    {
                        new RaceTime
                        {
                            InstanceName = "Primary",
                            ApplianceName = "MYLAPS X2",
                            ApplianceInstanceName = "Master",
                            How = "Optical",
                            Time = new TimeSpan(0, 0, 0, 36, 401),
                            TimeInfo = TimeInfo.PersonalBest
                        },
                        new RaceTime
                        {
                            InstanceName = "Secondary",
                            ApplianceName = "MYLAPS X2",
                            ApplianceInstanceName = "Master",
                            How = "Transponder",
                            Time = new TimeSpan(0, 0, 0, 36, 403),
                            TimeInfo = TimeInfo.PersonalBest
                        }
                    },
                    Laps = new Collection<RaceLap>
                    {
                        new RaceLap
                        {
                            InstanceName = "Primary",
                            ApplianceName = "MYLAPS X2",
                            ApplianceInstanceName = "Master",
                            How = "Optical",
                            Time = new TimeSpan(0, 0, 0, 9, 641),
                            When = DateTime.Now,
                            Flags = RaceEventFlags.Measured | RaceEventFlags.Present
                        },
                        new RaceLap
                        {
                            InstanceName = "Primary",
                            ApplianceName = "MYLAPS X2",
                            ApplianceInstanceName = "Master",
                            How = "Optical",
                            Time = new TimeSpan(0, 0, 0, 36, 401),
                            When = DateTime.Now,
                            Flags = RaceEventFlags.Measured | RaceEventFlags.Present
                        },
                        new RaceLap
                        {
                            InstanceName = "Primary",
                            ApplianceName = "MYLAPS X2",
                            ApplianceInstanceName = "Master",
                            How = "Transponder",
                            Time = new TimeSpan(0, 0, 0, 9, 643),
                            When = DateTime.Now,
                            Flags = RaceEventFlags.Measured
                        },
                        new RaceLap
                        {
                            InstanceName = "Primary",
                            ApplianceName = "MYLAPS X2",
                            ApplianceInstanceName = "Master",
                            How = "Transponder",
                            Time = new TimeSpan(0, 0, 0, 36, 403),
                            When = DateTime.Now,
                            Flags = RaceEventFlags.Measured
                        },
                        new RaceLap
                        {
                            InstanceName = "Secondary",
                            ApplianceName = "MYLAPS X2",
                            ApplianceInstanceName = "Master",
                            How = "Transponder",
                            Time = new TimeSpan(0, 0, 0, 9, 643),
                            When = DateTime.Now,
                            Flags = RaceEventFlags.Measured | RaceEventFlags.Present
                        },
                        new RaceLap
                        {
                            InstanceName = "Secondary",
                            ApplianceName = "MYLAPS X2",
                            ApplianceInstanceName = "Master",
                            How = "Transponder",
                            Time = new TimeSpan(0, 0, 0, 36, 403),
                            When = DateTime.Now,
                            Flags = RaceEventFlags.Measured | RaceEventFlags.Present
                        }
                    }
                },
                new Race
                {
                    Id = new Guid("{7D5F3799-14C1-4562-B8E4-8C7B734CAF38}"),
                    DistanceId = distances[0].Id,
                    CompetitorId = competitors[1].Id,
                    Heat = 1,
                    Lane = 1,
                    Color = 1
                },
                new Race
                {
                    Id = new Guid("{425E0181-3E1A-4619-B1C5-410CECBA68C8}"),
                    DistanceId = distances[0].Id,
                    CompetitorId = competitors[2].Id,
                    Heat = 2,
                    Lane = 0,
                    Color = 0
                },
                new Race
                {
                    Id = new Guid("{133671A3-679F-4C84-A818-B5839BBCE478}"),
                    DistanceId = distances[0].Id,
                    CompetitorId = competitors[3].Id,
                    Heat = 3,
                    Lane = 1,
                    Color = 1
                },
                new Race
                {
                    Id = new Guid("{C9393080-A58C-43AF-BDC4-1BD62D8792C1}"),
                    DistanceId = distances[1].Id,
                    CompetitorId = competitors[0].Id,
                    Heat = 1,
                    Lane = 0,
                    Color = 0
                },
                new Race
                {
                    Id = new Guid("{241BC86F-EAA3-4E0D-BDF5-EDBFDD9ACF39}"),
                    DistanceId = distances[1].Id,
                    CompetitorId = competitors[1].Id,
                    Heat = 1,
                    Lane = 1,
                    Color = 1
                },
                new Race
                {
                    Id = new Guid("{E58A1AC8-CEDC-4DA9-98F5-B301E7C5E2C9}"),
                    DistanceId = distances[1].Id,
                    CompetitorId = competitors[2].Id,
                    Heat = 2,
                    Lane = 0,
                    Color = 2
                },
                new Race
                {
                    Id = new Guid("{4C92EE78-2B36-487B-9B2B-51C26F722F4E}"),
                    DistanceId = distances[1].Id,
                    CompetitorId = competitors[3].Id,
                    Heat = 2,
                    Lane = 1,
                    Color = 3
                }
            };

            context.DistanceCombinations.AddOrUpdate(d => d.Id, distanceCombinations);
            context.Races.AddOrUpdate(d => d.Id, races);
#endif
        }
    }
}