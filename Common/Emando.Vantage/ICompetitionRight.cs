namespace Emando.Vantage
{
    public interface ICompetitionRight
    {
        string LicenseIssuerId { get; }

        string Discipline { get; }

        int CompetitionClass { get; }

        string Value { get; }

        string RoleName { get; }
    }
}