using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Competitions
{
    public class InstanceResultViewModel
    {
        public InstanceResultViewModel(string instanceName, RaceResult result, RaceTime time, bool isPresented = false)
        {
            this.InstanceName = instanceName;
            this.Result = result;
            this.Time = time;
            this.IsPresented = isPresented;
        }

        public string InstanceName { get; }

        public RaceResult Result { get; }

        public RaceTime Time { get; }

        public bool IsPresented { get; }
    }
}