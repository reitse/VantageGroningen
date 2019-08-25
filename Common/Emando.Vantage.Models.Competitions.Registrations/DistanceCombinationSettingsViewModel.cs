using System;
using Emando.Vantage.Competitions.Registrations;

namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class DistanceCombinationSettingsViewModel
    {
        public Guid DistanceCombinationId { get; set; }

        public DateTime? Opens { get; set; }

        public DateTime? Closes { get; set; }

        public decimal? Price { get; set; }

        public AllowedRegistrations AllowedRegistrations { get; set; }

        public InviteeViewModel[] Invitees { get; set; }

        public int? MaxCompetitors { get; set; }

        public bool RequireVenueSubscription { get; set; }

        public string LimitTimeDistanceDiscipline { get; set; }

        public int? LimitTimeDistanceValue { get; set; }

        public TimeSpan? LimitTime { get; set; }

        public string ThresholdTimeDistanceDiscipline { get; set; }

        public int? ThresholdTimeDistanceValue { get; set; }

        public TimeSpan? ThresholdTime { get; set; }

        public string ClubCodeFilter { get; set; }

        public string HomeVenueFilter { get; set; }
    }
}