namespace Emando.Vantage.Models
{
    public class PersonCategoryViewModel : IPersonCategory
    {
        public string LicenseIssuerId { get; set; }

        public string Discipline { get; set; }

        public int FromAge { get; set; }

        public int ToAge { get; set; }

        public Gender Gender { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public PersonCategoryFlags Flags { get; set; }
    }
}