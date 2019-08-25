namespace Emando.Vantage
{
    public static class VantageRoles
    {
        #region Nested type: Competitions

        public static class Competitions
        {
            private const string Prefix = "Competitions.";
            public const string Administrator = Prefix + "Administrator";
            public const string Editor = Prefix + "Editor";
            public const string Registrar = Prefix + "Registrar";
            public const string DistancePointsEditor = Prefix + "DistancePointsEditor";
        }

        #endregion

        #region Nested type: CompetitionSeries

        public static class CompetitionSeries
        {
            private const string Prefix = "CompetitionSeries.";
            public const string Administrator = Prefix + "Administrator";
            public const string Editor = Prefix + "Editor";
        }

        #endregion

        #region Nested type: PersonLicenses

        public static class PersonLicenses
        {
            private const string Prefix = "PersonLicenses.";
            public const string Issuer = Prefix + "Issuer";
            public const string DetailsViewer = Prefix + "DetailsViewer";
        }

        #endregion

        #region Nested type: Transponders

        public static class Transponders
        {
            private const string Prefix = "Transponders.";
            public const string Administrator = Prefix + "Administrator";
        }

        #endregion

        #region Nested type: Users

        public static class Users
        {
            private const string Prefix = "Users.";
            public const string Registrar = Prefix + "Registrar";
        }

        #endregion

        public static class Reports
        {
            private const string Prefix = "Reports.";
            public const string TemplateEditor = Prefix + "TemplateEditor";
        }

        public const string StatisticsViewer = "StatisticsViewer";
    }
}