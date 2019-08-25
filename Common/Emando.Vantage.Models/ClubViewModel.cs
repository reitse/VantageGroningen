namespace Emando.Vantage.Models
{
    public class ClubViewModel : IClub
    {
        public string CountryCode { get; set; }

        public int Code { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }
    }
}