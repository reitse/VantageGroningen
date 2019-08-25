using System.Windows;
using System.Windows.Controls;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    public class PairsRaceBall : Control
    {
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(int), typeof(PairsRaceBall), new PropertyMetadata(null));

        static PairsRaceBall()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PairsRaceBall), new FrameworkPropertyMetadata(typeof(PairsRaceBall)));
        }

        public int Color
        {
            get { return (int)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
    }
}