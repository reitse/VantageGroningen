using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    public class PairsRaceBallRow : ItemsControl
    {
        public static readonly DependencyProperty DropTargetProperty = DependencyProperty.Register("DropTarget", typeof(IDropTarget), typeof(PairsRaceBallRow),
            new PropertyMetadata(null));

        public static readonly DependencyProperty IsFinishProperty = DependencyProperty.Register("IsFinish", typeof(bool), typeof(PairsRaceBallRow),
            new PropertyMetadata(false));

        static PairsRaceBallRow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PairsRaceBallRow), new FrameworkPropertyMetadata(typeof(PairsRaceBallRow)));
        }

        public bool IsFinish
        {
            get { return (bool)GetValue(IsFinishProperty); }
            set { SetValue(IsFinishProperty, value); }
        }

        public IDropTarget DropTarget
        {
            get { return (IDropTarget)GetValue(DropTargetProperty); }
            set { SetValue(DropTargetProperty, value); }
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            for (var i = 0; i < Items.Count; i++)
            {
                var container = ItemContainerGenerator.ContainerFromIndex(i) as UIElement;
                if (container != null)
                {
                    var position = IsFinish ? 3 - i : i;
                    Grid.SetColumn(container, position);
                }
            }
        }
    }
}