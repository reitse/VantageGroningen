using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public class DistanceTimingState<TCompetition, TDistance, TRace, TRacePassing, TRaceLap>
        where TCompetition : class, ICompetition
        where TDistance : class, IDistance
        where TRace : IRaceState<TRacePassing, TRaceLap>
        where TRacePassing : IReadOnlyRacePassing, IHaveRacePassingKey
        where TRaceLap : IReadOnlyActiveRaceLap, IHaveRacePassingKey
    {
        private readonly Dictionary<Heat, HeatState<TRace, TRacePassing, TRaceLap>> activeHeats = new Dictionary<Heat, HeatState<TRace, TRacePassing, TRaceLap>>();
        private readonly IDistanceDisciplineCalculatorManager calculatorManager;
        private readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

        public DistanceTimingState(IDistanceDisciplineCalculatorManager calculatorManager)
        {
            this.calculatorManager = calculatorManager;
        }

        public string InstanceName { get; private set; }

        public TCompetition Competition { get; private set; }

        public TDistance Distance { get; private set; }

        public IReadOnlyDictionary<Heat, HeatState<TRace, TRacePassing, TRaceLap>> ActiveHeats => activeHeats;

        public void EnterReadLock()
        {
            locker.EnterReadLock();
        }

        public bool TryEnterReadLock(int millisecondsTimeout)
        {
            return locker.TryEnterReadLock(millisecondsTimeout);
        }

        public void EnterWriteLock()
        {
            locker.EnterWriteLock();
        }

        public void ExitReadLock()
        {
            locker.ExitReadLock();
        }

        public void ExitWriteLock()
        {
            locker.ExitWriteLock();
        }

        public bool IsReadLockHeld => locker.IsReadLockHeld;

        public bool IsWriteLockHeld => locker.IsWriteLockHeld;

        public void SwitchInstance(string instanceName)
        {
            DeactivateDistance();
            Competition = null;
            InstanceName = instanceName;
        }

        public void ActivateCompetition(TCompetition competition)
        {
            Competition = competition;
        }

        public void ActivateDistance(TDistance activeDistance)
        {
            Distance = activeDistance;
        }

        public void DeactivateDistance()
        {
            DeactivateHeats();
            Distance = null;
        }

        public void DeactivateHeats()
        {
            foreach (var heat in activeHeats.Keys.ToList())
                DeactivateHeat(heat);
        }

        public void ActivateHeat(Heat heat, IEnumerable<TRace> races)
        {
            var heatState = new HeatState<TRace, TRacePassing, TRaceLap>(Distance, heat, races, calculatorManager.Get(Distance.Discipline));
            heatState.Activate();
            activeHeats[heat] = heatState;
        }

        public void DeactivateHeat(Heat heat)
        {
            if (!activeHeats.ContainsKey(heat))
                return;

            var heatState = activeHeats[heat];
            heatState.Deactivate();
            activeHeats.Remove(heat);
        }

        public void StartHeat(Heat heat, TimeSpan clock)
        {
            var heatState = activeHeats[heat];
            heatState.Start(clock);
        }

        public void ClearHeat(Heat heat)
        {
            var heatState = activeHeats[heat];
            heatState.Clear();
        }

        public void CommitHeat(Heat heat)
        {
            // The heat committed event is an event that may be fired regardless of heat activation
            // It will be ignored because this object only keeps track of the active heat
            if (!activeHeats.ContainsKey(heat))
                return;

            var heatState = activeHeats[heat];
            heatState.Commit();
        }

        public void SetHeatNextLapIndex(Heat heat, int index)
        {
            var heatState = activeHeats[heat];
            heatState.SetNextLapIndex(index);
        }

        public void AddRaceLap(Heat heat, Guid raceId, TRaceLap lap)
        {
            var heatState = activeHeats[heat];
            heatState.AddRaceLap(raceId, lap);
        }

        public void UpdateRaceLap(Heat heat, Guid raceId, PresentationSource presentationSource, TimeSpan oldTime, TRaceLap update)
        {
            // The race lap updated event is an event that may be fired regardless of heat activation
            // because it may be called for ranking changes in other heats
            // It will be ignored because this object only keeps track of the active heat
            if (!activeHeats.ContainsKey(heat))
                return;

            var heatState = activeHeats[heat];
            heatState.UpdateRaceLap(raceId, presentationSource, oldTime, update);
        }

        public void AddRacePassing(Heat heat, Guid raceId, TRacePassing passing)
        {
            var heatState = activeHeats[heat];
            heatState.AddRacePassing(raceId, passing);
        }

        public void UpdateRacePassing(Heat heat, Guid raceId, PresentationSource presentationSource, TimeSpan oldTime, TRacePassing update)
        {
            var heatState = activeHeats[heat];
            heatState.UpdateRacePassing(raceId, presentationSource, oldTime, update);
        }

        public void UpdateRaceSpeed(Heat heat, Guid raceId, TRacePassing passing)
        {
            var heatState = activeHeats[heat];
            heatState.UpdateRaceSpeed(raceId, passing);
        }

        public void SetRaceNextLapIndex(Heat heat, Guid raceId, int index)
        {
            var heatState = activeHeats[heat];
            heatState.SetRaceNextLapIndex(raceId, index);
        }
    }
}