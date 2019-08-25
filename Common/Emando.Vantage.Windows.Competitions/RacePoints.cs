using System.Linq;
using Caliburn.Micro;

namespace Emando.Vantage.Windows.Competitions
{
    public class RacePoints : PropertyChangedBase
    {
        private readonly IObservableCollection<RaceLapsGroup> groups;

        public RacePoints(IObservableCollection<RaceLapsGroup> groups)
        {
            this.groups = groups;

            groups.CollectionChanged += (s, e) => Refresh();
        }

        public RaceIndexPoints? this[int index]
        {
            get
            {
                var group = groups.ElementAtOrDefault(index);
                if (group != null && group.Presented != null && group.Presented.Points.HasValue)
                    return new RaceIndexPoints(group.Presented.Time, group.Presented.Points.Value);

                return null;
            }
        }

        public decimal Total
        {
            get { return groups.Where(g => g.Presented != null).Sum(p => p.Presented.Points ?? 0); }
        }
    }
}