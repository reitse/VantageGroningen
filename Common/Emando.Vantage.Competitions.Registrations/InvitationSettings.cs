using System;

namespace Emando.Vantage.Competitions.Registrations
{
    public class InvitationSettings
    {
        public InvitationSettings()
        {
        }

        public InvitationSettings(bool sendInvitation, string introduction, string footer)
        {
            SendInvitation = sendInvitation;
            Introduction = introduction;
            Footer = footer;
        }

        public bool SendInvitation { get; set; }

        public string Introduction { get; set; }

        public string Footer { get; set; }
    }
}