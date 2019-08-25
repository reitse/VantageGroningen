namespace Emando.Vantage.Models.Competitions
{
    public class RankedPersonPointsViewModel
    {
        public int Ranking { get; set; }

        public string LicenseIssuerId { get; set; }

        public string LicenseDiscipline { get; set; }

        public string LicenseKey { get; set; }

        public NameViewModel LicensePersonName { get; set; }

        public string LicenseCategory { get; set; }

        public string LicenseClubFullName { get; set; }

        public string LicenseVenueCode { get; set; }

        public decimal Points { get; set; }

        public PersonTimeViewModel FirstTime { get; set; }

        public PersonTimeViewModel SecondTime { get; set; }

        public PersonTimeViewModel ThirdTime { get; set; }

        public PersonTimeViewModel FourthTime { get; set; }
    }
}