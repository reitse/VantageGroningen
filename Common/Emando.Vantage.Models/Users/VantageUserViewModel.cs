using System;

namespace Emando.Vantage.Models.Users
{
    public class VantageUserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public NameViewModel Name { get; set; }

        public DateTime? LastLogin { get; set; }

        public bool IsLockedOut { get; set; }

        public DateTime? LockoutEndDate { get; set; }
    }
}