using System;

namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class InviteeViewModel
    {
        public Guid DistanceCombinationId { get; set; }

        public string LicenseIssuerId { get; set; }

        public string LicenseDiscipline { get; set; }

        public string LicenseKey { get; set; }

        public string Email { get; set; }

        public bool IsInvited { get; set; }

        public int? Reserve { get; set; }
    }
}