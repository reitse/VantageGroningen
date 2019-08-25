namespace Emando.Vantage
{
    public static class VantageFeatures
    {
        public static class Competitions
        {
            private const string Prefix = "Competition.";
            public const string EditResultsStatus = Prefix + "EditResultsStatus";
            public const string Registration = Prefix + "Registration";
        }

        public static class PersonLicense
        {
            private const string Prefix = "PersonLicense.";
            public const string Edit = Prefix + "Edit";
            public const string CreateDisposable = Prefix + "CreateDisposable";
        }

        public static class Venue
        {
            private const string Prefix = "Venue.";
            public const string Subscriptions = Prefix + "Subscriptions";
            public const string Classes = Prefix + "Classes";
        }

        public static class Report
        {
            private const string Prefix = "Report.";
            public const string Templates = Prefix + "Templates";
        }

        public const string Rankings = "Rankings";
    }
}