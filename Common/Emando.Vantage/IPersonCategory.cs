namespace Emando.Vantage
{
    public interface IPersonCategory
    {
        string LicenseIssuerId { get; set; }

        string Discipline { get; set; }

        int FromAge { get; set; }

        int ToAge { get; set; }

        Gender Gender { get; set; }

        string Code { get; set; }

        string Name { get; set; }

        PersonCategoryFlags Flags { get; set; }
    }
}