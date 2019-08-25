using AutoMapper;
using Emando.Vantage.Api.Models.Competitions;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Api.Client.Competitions
{
    public static class CompetitionsModelsMappingConfig
    {
        public static void Register()
        {
            ModelsMappingConfig.Register();
            Mapper.CreateMap<ICompetition, CompetitionCreateModel>();
            Mapper.CreateMap<ICompetition, CompetitionUpdateModel>();
            Mapper.AssertConfigurationIsValid();
        }
    }
}