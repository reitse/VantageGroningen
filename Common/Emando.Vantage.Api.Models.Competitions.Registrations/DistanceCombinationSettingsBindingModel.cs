using System;
using System.ComponentModel.DataAnnotations;
using Emando.Vantage.Competitions.Registrations;

namespace Emando.Vantage.Api.Models.Competitions.Registrations
{
    public class DistanceCombinationSettingsBindingModel
    {
        public Guid DistanceCombinationId { get; set; }

        public DateTime? Opens { get; set; }

        public DateTime? Closes { get; set; }

        public decimal? Price { get; set; }

        public AllowedRegistrations AllowedRegistrations { get; set; }

        public InviteeBindingModel[] Invitees { get; set; }

        [Range(1, int.MaxValue)]
        public int? MaxCompetitors { get; set; }

        public bool RequireVenueSubscription { get; set; }

        [StringLength(100)]
        public string LimitTimeDistanceDiscipline { get; set; }

        [Range(0, int.MaxValue)]
        public int? LimitTimeDistanceValue { get; set; }

        public TimeSpan? LimitTime { get; set; }

        [StringLength(100)]
        public string ThresholdTimeDistanceDiscipline { get; set; }

        [Range(0, int.MaxValue)]
        public int? ThresholdTimeDistanceValue { get; set; }

        public TimeSpan? ThresholdTime { get; set; }

        [StringLength(100)]
        public string ClubCodeFilter { get; set; }

        [StringLength(100)]
        public string HomeVenueFilter { get; set; }
    }
}