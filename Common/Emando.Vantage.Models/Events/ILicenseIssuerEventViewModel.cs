namespace Emando.Vantage.Models.Events
{
    public interface ILicenseIssuerEventViewModel : IEventViewModel
    {
        string LicenseIssuerId { get; }
    }
}