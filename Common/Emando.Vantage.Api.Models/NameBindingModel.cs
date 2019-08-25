using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class NameBindingModel
    {
        [StringLength(20)]
        public string Initials { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(20)]
        public string SurnamePrefix { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        public void SetDefaultCasing()
        {
            if (Initials != null)
                Initials = Initials.ToUpper();
            if (FirstName != null)
                FirstName = FirstName.ToInvariantTitleCase();
            if (SurnamePrefix != null)
                SurnamePrefix = SurnamePrefix.ToLower();
            if (Surname != null)
                Surname = Surname.ToInvariantTitleCase();
        }
    }
}