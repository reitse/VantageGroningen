using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emando.Vantage.Entities.Identity
{
    public class UserSetting : IUserSetting
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        public virtual VantageUser User { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(100)]
        public string Type { get; set; }

        [StringLength(200)]
        [Key, Column(Order = 2)]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}