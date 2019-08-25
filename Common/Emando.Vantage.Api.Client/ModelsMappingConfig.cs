using AutoMapper;
using Emando.Vantage.Api.Models;

namespace Emando.Vantage.Api.Client
{
    public static class ModelsMappingConfig
    {
        public static void Register()
        {
            Mapper.CreateMap<IClub, ClubCreateModel>();
            Mapper.CreateMap<IClub, ClubUpdateModel>();
            Mapper.AssertConfigurationIsValid();
        }
    }
}