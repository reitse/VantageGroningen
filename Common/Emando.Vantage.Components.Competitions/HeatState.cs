using System;
using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public class HeatState<TRace, TRacePassing, TRaceLap>
        where TRace : IRaceState<TRacePassing, TRaceLap>
        where TRacePassing : IReadOnlyRacePassing, IHaveRacePassingKey
        where TRaceLap : IReadOnlyActiveRaceLap, IHaveRacePassingKey
    {
        private readonly IDistance distance;
        private readonly IDistanceDisciplineCalculator calculator;
        private readonly IDictionary<Guid, IList<TRaceLap>> laps = new Dictionary<Guid, IList<TRaceLap>>();
        private readonly IDictionary<Guid, IList<TRacePassing>> passings = new Dictionary<Guid, IList<TRacePassing>>();
        private readonly IDictionary<Guid, int> nextLapIndices = new Dictionary<Guid, int>(); 
        private readonly IDictionary<Guid, IList<CalculatedLap>> calculatedLaps = new Dictionary<Guid, IList<CalculatedLap>>();
        private readonly IDictionary<Guid, TRacePassing> speeds = new Dictionary<Guid, TRacePassing>(); 
        private int nextLapIndex;

        public HeatState(IDistance distance, Heat number, IEnumerable<TRace> races, IDistanceDisciplineCalculator calculator)
        {
            this.distance = distance;
            this.calculator = calculator;

            Number = number;
            Races = races.ToList();

            foreach (var race in Races)
            {
                laps.Add(race.RaceId, race.Laps.ToList());
                calculatedLaps.Add(race.RaceId, calculator.CalculateLaps(distance, race.Laps.Presented().Cast<IReadOnlyActiveRaceLap>()).ToList());
                passings.Add(race.RaceId, race.Passings.ToList());
                nextLapIndices.Add(race.RaceId, 0);
            }
        }

        public Heat Number { get; private set; }

        public RaceStatus Status { get; private set; }

        public DateTime? Started { get; private set; }

        public IList<TRace> Races { get; }

        public void Activate()
        {
            Status = RaceStatus.Activated;
        }

        public void Start(TimeSpan clock)
        {
            Started = DateTime.UtcNow - clock;
            Status = RaceStatus.Running;
        }

        public void Clear()
        {
            foreach (var race in Races)
            {
                laps[race.RaceId].Clear();
                calculatedLaps[race.RaceId].Clear();
                passings[race.RaceId].Clear();
                speeds.Remove(race.RaceId);
                nextLapIndices[race.RaceId] = 0;
            }

            Started = null;
            Status = RaceStatus.Activated;
        }

        public void Deactivate()
        {
            Status = RaceStatus.Deactivated;
        }

        public void Commit()
        {
        }

        public void SetNextLapIndex(int index)
        {
            nextLapIndex = index;
        }

        public void AddRaceLap(Guid raceId, TRaceLap lap)
        {
            var raceLaps = laps[raceId];
            raceLaps.Add(lap);

            calculatedLaps[raceId] = calculator.CalculateLaps(distance, raceLaps.Presented().Cast<IReadOnlyActiveRaceLap>()).ToList();
        }

        public void UpdateRaceLap(Guid raceId, PresentationSource presentationSource, TimeSpan oldTime, TRaceLap update)
        {
            var raceLaps = laps[raceId];
            var old = raceLaps.Find(oldTime, presentationSource: presentationSource);
            if (raceLaps.Remove(old))
            {
                raceLaps.Add(update);
                calculatedLaps[raceId] = calculator.CalculateLaps(distance, raceLaps.Presented().Cast<IReadOnlyActiveRaceLap>()).ToList();
            }
        }

        public void AddRacePassing(Guid raceId, TRacePassing passing)
        {
            passings[raceId].Add(passing);
        }

        public void UpdateRacePassing(Guid raceId, PresentationSource presentationSource, TimeSpan oldTime, TRacePassing update)
        {
            var racePassings = passings[raceId];
            var old = racePassings.Find(oldTime, presentationSource: presentationSource);
            if (racePassings.Remove(old))
                racePassings.Add(update);
        }

        public TRace GetRace(Guid raceId)
        {
            return Races.Single(r => r.RaceId == raceId);
        }

        public TRacePassing GetSpeed(Guid raceId)
        {
            TRacePassing passing;
            return speeds.TryGetValue(raceId, out passing) ? passing : default(TRacePassing);
        }

        public IList<CalculatedLap> GetCalculatedLaps(Guid raceId)
        {
            return calculatedLaps[raceId];
        }

        //public IList<TRacePassing> GetPassings(Guid raceId)
        //{
        //    return passings[raceId];
        //}

        public void SetRaceNextLapIndex(Guid raceId, int index)
        {
            nextLapIndices[raceId] = index;
        }

        public void UpdateRaceSpeed(Guid raceId, TRacePassing passing)
        {
            speeds[raceId] = passing;
        }
    }
}