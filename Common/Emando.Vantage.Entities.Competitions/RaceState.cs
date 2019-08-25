using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2015/07/Entities/Competitions")]
    public class RaceState : IRaceState<RacePassingState, RaceLapState>
    {
        public RaceState(Race race, ReadOnlyCollection<RaceTransponder> transponders, RaceStart start, RaceResult result, ReadOnlyCollection<RacePassingState> passings,
            RaceTime time, ReadOnlyCollection<RaceLapState> laps, ReadOnlyCollection<CalculatedLap> estimatedLaps)
        {
            if (race == null)
                throw new ArgumentNullException(nameof(race));

            Race = race;
            RaceId = race.Id;
            Transponders = transponders;
            Start = start;
            Result = result;
            Passings = passings;
            Time = time;
            Laps = laps;
            EstimatedLaps = estimatedLaps;
        }

        public Race Race { get; private set; }

        [DataMember]
        public ReadOnlyCollection<RaceTransponder> Transponders { get; private set; }

        [DataMember]
        public RaceStart Start { get; private set; }

        [DataMember]
        public RaceResult Result { get; private set; }

        [DataMember]
        public ReadOnlyCollection<RacePassingState> Passings { get; private set; }

        [DataMember]
        public RaceTime Time { get; private set; }

        [DataMember]
        public ReadOnlyCollection<RaceLapState> Laps { get; private set; }

        [DataMember]
        public ReadOnlyCollection<CalculatedLap> EstimatedLaps { get; private set; }

        #region IRaceState<RacePassingState,RaceLapState> Members

        [DataMember]
        public Guid RaceId { get; private set; }

        IEnumerable<RacePassingState> IRaceState<RacePassingState, RaceLapState>.Passings => Passings;

        IEnumerable<RaceLapState> IRaceState<RacePassingState, RaceLapState>.Laps => Laps;

        #endregion

        public static RaceState FromRace(Race race, RaceStatus status, TimeInvalidReason? timeInvalidReason, string instanceName)
        {
            var transponders = race.Transponders.ToList().AsReadOnly();
            var start = race.Starts.OrderBy(s => s.When).LastOrDefault(s => s.InstanceName == instanceName);
            var result = race.Results.SingleOrDefault(r => r.InstanceName == instanceName) ?? new RaceResult
            {
                RaceId = race.Id,
                InstanceName = instanceName,
                Status = status,
                TimeInvalidReason = timeInvalidReason
            };
            var passings = race.Passings?.Where(p => p.InstanceName == instanceName).Select(RacePassingState.FromPassing).ToList().AsReadOnly();
            var time = race.Times.SingleOrDefault(r => r.InstanceName == instanceName);
            var laps = race.Laps?.Where(l => l.InstanceName == instanceName).Select(l => RaceLapState.FromLap(l)).ToList().AsReadOnly();
            var estimatedLaps = race.EstimatedLaps?.ToList().AsReadOnly();
            return new RaceState(race, transponders, start, result, passings, time, laps, estimatedLaps);
        }
    }
}