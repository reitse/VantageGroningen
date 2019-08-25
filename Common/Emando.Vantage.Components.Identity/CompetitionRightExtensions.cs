namespace Emando.Vantage.Components.Identity
{
    public static class CompetitionRightExtensions
    {
        public static string Encode(this ICompetitionRight right)
        {
            return Encode(right.LicenseIssuerId, right.Discipline, right.CompetitionClass, right.Value, right.RoleName);
        }

        private static string Encode(string licenseIssuerId, string discipline, int competitionClass, string value, string role)
        {
            return $"{licenseIssuerId}/{discipline}/{competitionClass}:{value}/{role}";
        }
    }
}