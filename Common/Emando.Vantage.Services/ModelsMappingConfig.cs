using System.Linq;
using AutoMapper;
using Emando.Vantage.Entities;
using Emando.Vantage.Events;
using Emando.Vantage.Models;
using Emando.Vantage.Models.Events;
using Emando.Vantage.Workflows.Events;

namespace Emando.Vantage.Services
{
    public static class ModelsMappingConfig
    {
        public static void Register()
        {
            Mapper.CreateMap<Person, PersonViewModel>();
            Mapper.CreateMap<Person, RestrictedPersonViewModel>();
            Mapper.CreateMap<Person, PersonDetailsViewModel>();
            Mapper.CreateMap<Person, PersonRestrictedDetailsViewModel>();
            Mapper.CreateMap<Name, NameViewModel>();
            Mapper.CreateMap<Contact, ContactViewModel>();
            Mapper.CreateMap<Address, AddressViewModel>();
            Mapper.CreateMap<Address, RestrictedAddressViewModel>();
            Mapper.CreateMap<Club, ClubViewModel>();
            Mapper.CreateMap<Venue, VenueViewModel>();
            Mapper.CreateMap<Venue, VenueDetailsViewModel>();
            Mapper.CreateMap<VenueDistrict, VenueDistrictViewModel>();
            Mapper.CreateMap<VenueTrack, VenueTrackViewModel>();
            Mapper.CreateMap<LicenseIssuer, LicenseIssuerViewModel>();
            Mapper.CreateMap<PersonLicense, PersonLicenseViewModel>();
            Mapper.CreateMap<PersonLicense, PersonLicenseDetailsViewModel>();
            Mapper.CreateMap<PersonLicense, PersonLicenseSummaryViewModel>();
            Mapper.CreateMap<PersonLicense, PersonLicenseRestrictedDetailsViewModel>();
            Mapper.CreateMap<PersonLicensePrice, PersonLicensePriceViewModel>();
            Mapper.CreateMap<PersonLicenseVenueSubscription, PersonLicenseVenueSubscriptionViewModel>();
            Mapper.CreateMap<PersonLicenseVenueClass, PersonLicenseVenueClassViewModel>();
            Mapper.CreateMap<PersonCategory, PersonCategoryViewModel>();
            Mapper.CreateMap<Transponder, TransponderViewModel>();
            Mapper.CreateMap<TransponderBag, TransponderBagViewModel>();
            Mapper.CreateMap<TransponderSet, TransponderSetViewModel>();
            Mapper.CreateMap<TransponderSetTransponder, TransponderSetTransponderViewModel>();
            Mapper.CreateMap<ReportTemplate, ReportTemplateViewModel>()
                .ForMember(m => m.LogoNames, o => o.ResolveUsing(t => t.Logos?.Select(l => l.Name).ToArray()));
            Mapper.CreateMap<IUserSetting, UserSettingViewModel>();

            Mapper.CreateMap<EventBase, EventViewModelBase>()
                .Include<PersonLicenseChangedEvent, PersonLicenseChangedEventViewModel>();

            Mapper.CreateMap<PersonLicenseChangedEvent, PersonLicenseChangedEventViewModel>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}