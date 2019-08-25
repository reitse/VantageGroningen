using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Windows.Competitions;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    public class Icerink : Control
    {
        public static readonly DependencyProperty PathProviderProperty = DependencyProperty.Register("PathProvider", typeof(IDistanceDisciplinePathProvider), typeof(Icerink),
            new PropertyMetadata(default(IDistanceDisciplinePathProvider)));
        public static readonly DependencyProperty LaneBrushProperty = DependencyProperty.Register("LaneBrush", typeof(Brush), typeof(Icerink),
            new PropertyMetadata(default(Brush)));
        public static readonly DependencyProperty WarmupLaneBrushProperty = DependencyProperty.Register("WarmupLaneBrush", typeof(Brush), typeof(Icerink),
            new PropertyMetadata(default(Brush)));
        public static readonly DependencyProperty WhiteStyleProperty = DependencyProperty.Register("WhiteStyle", typeof(Style), typeof(Icerink),
            new PropertyMetadata(default(Style)));
        public static readonly DependencyProperty RedStyleProperty = DependencyProperty.Register("RedStyle", typeof(Style), typeof(Icerink),
            new PropertyMetadata(default(Style)));
        public static readonly DependencyProperty YellowStyleProperty = DependencyProperty.Register("YellowStyle", typeof(Style), typeof(Icerink),
            new PropertyMetadata(default(Style)));
        public static readonly DependencyProperty BlueStyleProperty = DependencyProperty.Register("BlueStyle", typeof(Style), typeof(Icerink),
            new PropertyMetadata(default(Style)));
        public static readonly DependencyProperty DistanceProperty = DependencyProperty.Register("Distance", typeof(Distance), typeof(Icerink), new PropertyMetadata(null));
        public static readonly DependencyProperty MarkLineBrushProperty = DependencyProperty.Register("MarkLineBrush", typeof(Brush), typeof(Icerink),
            new PropertyMetadata(default(Brush)));
        public static readonly DependencyProperty LineThicknessProperty = DependencyProperty.Register("LineThickness", typeof(double), typeof(Icerink),
            new PropertyMetadata(0.5));
        public static readonly DependencyProperty WhiteProperty = DependencyProperty.Register("White", typeof(IActiveTrackRaceViewModel), typeof(Icerink),
            new PropertyMetadata(null, OnWhiteChanged));
        public static readonly DependencyProperty RedProperty = DependencyProperty.Register("Red", typeof(IActiveTrackRaceViewModel), typeof(Icerink),
            new PropertyMetadata(null, OnRedChanged));
        public static readonly DependencyProperty YellowProperty = DependencyProperty.Register("Yellow", typeof(IActiveTrackRaceViewModel), typeof(Icerink),
            new PropertyMetadata(null, OnYellowChanged));
        public static readonly DependencyProperty BlueProperty = DependencyProperty.Register("Blue", typeof(IActiveTrackRaceViewModel), typeof(Icerink),
            new PropertyMetadata(null, OnBlueChanged));
        private readonly IDictionary<PairsRaceColor, Shape> skaters = new Dictionary<PairsRaceColor, Shape>();
        private readonly IDictionary<PairsRaceColor, TrackCompetitorStoryboard> storyboards = new Dictionary<PairsRaceColor, TrackCompetitorStoryboard>();
        private Canvas canvas;

        static Icerink()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Icerink), new FrameworkPropertyMetadata(typeof(Icerink)));
        }

        public Distance Distance
        {
            get { return (Distance)GetValue(DistanceProperty); }
            set { SetValue(DistanceProperty, value); }
        }

        public Style WhiteStyle
        {
            get { return (Style)GetValue(WhiteStyleProperty); }
            set { SetValue(WhiteStyleProperty, value); }
        }

        public Style RedStyle
        {
            get { return (Style)GetValue(RedStyleProperty); }
            set { SetValue(RedStyleProperty, value); }
        }

        public Style YellowStyle
        {
            get { return (Style)GetValue(YellowStyleProperty); }
            set { SetValue(YellowStyleProperty, value); }
        }

        public Style BlueStyle
        {
            get { return (Style)GetValue(BlueStyleProperty); }
            set { SetValue(BlueStyleProperty, value); }
        }

        public IActiveTrackRaceViewModel White
        {
            get { return (IActiveTrackRaceViewModel)GetValue(WhiteProperty); }
            set { SetValue(WhiteProperty, value); }
        }

        public IActiveTrackRaceViewModel Red
        {
            get { return (IActiveTrackRaceViewModel)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }

        public IActiveTrackRaceViewModel Yellow
        {
            get { return (IActiveTrackRaceViewModel)GetValue(YellowProperty); }
            set { SetValue(YellowProperty, value); }
        }

        public IActiveTrackRaceViewModel Blue
        {
            get { return (IActiveTrackRaceViewModel)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        public double LineThickness
        {
            get { return (double)GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }

        public Brush MarkLineBrush
        {
            get { return (Brush)GetValue(MarkLineBrushProperty); }
            set { SetValue(MarkLineBrushProperty, value); }
        }

        public Brush LaneBrush
        {
            get { return (Brush)GetValue(LaneBrushProperty); }
            set { SetValue(LaneBrushProperty, value); }
        }

        public Brush WarmupLaneBrush
        {
            get { return (Brush)GetValue(WarmupLaneBrushProperty); }
            set { SetValue(WarmupLaneBrushProperty, value); }
        }

        public IDistanceDisciplinePathProvider PathProvider
        {
            get { return (IDistanceDisciplinePathProvider)GetValue(PathProviderProperty); }
            set { SetValue(PathProviderProperty, value); }
        }

        private void OnSkaterChanged(PairsRaceColor color, IActiveTrackRaceViewModel oldRace, IActiveTrackRaceViewModel newRace, Style style)
        {
            if (Distance == null || PathProvider == null || canvas == null)
                return;

            TrackCompetitorStoryboard storyboard;
            if (storyboards.TryGetValue(color, out storyboard))
            {
                storyboard.DetachAndStop();
                storyboards.Remove(color);
            }

            Shape shape;
            if (skaters.TryGetValue(color, out shape))
            {
                canvas.Children.Remove(shape);
                skaters.Remove(color);
            }

            if (oldRace != null)
                oldRace.TimeInvalidReasonChanged -= RaceTimeInvalidReasonChanged;

            if (newRace == null)
                return;

            newRace.TimeInvalidReasonChanged += RaceTimeInvalidReasonChanged;
            if (newRace.TimeInvalidReason != null)
                return;

            var path = PathProvider.CreatePath(Distance, newRace.Lane);
            if (path == null)
                return;

            var ellipse = new EllipseGeometry(path.Figures[0].StartPoint, 2, 2);
            var ball = new Path
            {
                Data = ellipse,
                Style = style
            };
            Panel.SetZIndex(ball, 4 - newRace.Color);
            NameScope.SetNameScope(ball, new NameScope());
            ball.RegisterName("Ellipse", ellipse);
            canvas.Children.Add(ball);

            var animation = new PointAnimationUsingPath
            {
                Duration = TimeSpan.FromSeconds(PathProvider.Calculator.Length(Distance)),
                PathGeometry = path
            };
            Storyboard.SetTargetName(animation, "Ellipse");
            Storyboard.SetTargetProperty(animation, new PropertyPath(EllipseGeometry.CenterProperty));
            storyboard = new TrackCompetitorStoryboard(newRace, PathProvider.Calculator);
            storyboard.Children.Add(animation);
            storyboard.AttachAndBegin(ball);

            skaters[color] = ball;
            storyboards[color] = storyboard;
        }

        private void RaceTimeInvalidReasonChanged(object sender, EventArgs e)
        {
            var race = (IActiveTrackRaceViewModel)sender;
            switch ((PairsRaceColor)race.Color)
            {
                case PairsRaceColor.White:
                    OnSkaterChanged(PairsRaceColor.White, White, White, WhiteStyle);
                    break;
                case PairsRaceColor.Red:
                    OnSkaterChanged(PairsRaceColor.Red, White, Red, RedStyle);
                    break;
                case PairsRaceColor.Yellow:
                    OnSkaterChanged(PairsRaceColor.Yellow, White, Yellow, YellowStyle);
                    break;
                case PairsRaceColor.Blue:
                    OnSkaterChanged(PairsRaceColor.Blue, White, Blue, BlueStyle);
                    break;
            }
        }

        public override void OnApplyTemplate()
        {
            canvas = GetTemplateChild("Canvas") as Canvas;
            OnSkaterChanged(PairsRaceColor.White, White, White, WhiteStyle);
            OnSkaterChanged(PairsRaceColor.Red, White, Red, RedStyle);
            OnSkaterChanged(PairsRaceColor.Yellow, White, Yellow, YellowStyle);
            OnSkaterChanged(PairsRaceColor.Blue, White, Blue, BlueStyle);
            base.OnApplyTemplate();
        }

        private static void OnWhiteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Icerink)d).OnSkaterChanged(PairsRaceColor.White, e.OldValue as IActiveTrackRaceViewModel, e.NewValue as IActiveTrackRaceViewModel,
                d.GetValue(WhiteStyleProperty) as Style);
        }

        private static void OnRedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Icerink)d).OnSkaterChanged(PairsRaceColor.Red, e.OldValue as IActiveTrackRaceViewModel, e.NewValue as IActiveTrackRaceViewModel,
                d.GetValue(RedStyleProperty) as Style);
        }

        private static void OnYellowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Icerink)d).OnSkaterChanged(PairsRaceColor.Yellow, e.OldValue as IActiveTrackRaceViewModel, e.NewValue as IActiveTrackRaceViewModel,
                d.GetValue(YellowStyleProperty) as Style);
        }

        private static void OnBlueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Icerink)d).OnSkaterChanged(PairsRaceColor.Blue, e.OldValue as IActiveTrackRaceViewModel, e.NewValue as IActiveTrackRaceViewModel,
                d.GetValue(BlueStyleProperty) as Style);
        }
    }
}