using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Emando.Vantage.Windows.Competitions
{
    public static class RoundPointsColumns
    {
        public static readonly DependencyProperty TimeInfoCellTemplateProperty =
            DependencyProperty.RegisterAttached("TimeInfoCellTemplate", typeof(DataTemplate), typeof(RoundPointsColumns), new PropertyMetadata(default(DataTemplate)));
        public static readonly DependencyProperty TimeFormatterProperty =
            DependencyProperty.RegisterAttached("TimeFormatter", typeof(IValueConverter), typeof(RoundPointsColumns), new PropertyMetadata(default(IValueConverter)));
        public static readonly DependencyProperty TimeDigitsProperty =
            DependencyProperty.RegisterAttached("TimeDigits", typeof(int), typeof(RoundPointsColumns), new PropertyMetadata(2));
        public static readonly DependencyProperty PointsElementStyleProperty =
            DependencyProperty.RegisterAttached("PointsElementStyle", typeof(Style), typeof(RoundPointsColumns), new PropertyMetadata(null));
        public static readonly DependencyProperty TimeElementStyleProperty =
            DependencyProperty.RegisterAttached("TimeElementStyle", typeof(Style), typeof(RoundPointsColumns), new PropertyMetadata(null));
        public static readonly DependencyProperty PointsHeaderStyleProperty =
            DependencyProperty.RegisterAttached("PointsHeaderStyle", typeof(Style), typeof(RoundPointsColumns), new PropertyMetadata(null));
        public static readonly DependencyProperty RoundPointsProperty =
            DependencyProperty.RegisterAttached("RoundPoints", typeof(ICollection), typeof(RoundPointsColumns), new PropertyMetadata(null, OnRoundPointsChanged));
        public static readonly DependencyProperty RoundPointsStartColumnProperty =
            DependencyProperty.RegisterAttached("RoundPointsStartColumn", typeof(int), typeof(RoundPointsColumns), new PropertyMetadata(0));

        public static int GetRoundPointsStartColumn(DependencyObject obj)
        {
            return (int)obj.GetValue(RoundPointsStartColumnProperty);
        }

        public static void SetRoundPointsStartColumn(DependencyObject obj, int value)
        {
            obj.SetValue(RoundPointsStartColumnProperty, value);
        }

        public static ICollection GetRoundPoints(DependencyObject obj)
        {
            return (ICollection)obj.GetValue(RoundPointsProperty);
        }

        public static void SetRoundPoints(DependencyObject obj, ICollection value)
        {
            obj.SetValue(RoundPointsProperty, value);
        }

        public static void SetPointsHeaderStyle(DependencyObject element, Style value)
        {
            element.SetValue(PointsHeaderStyleProperty, value);
        }

        public static Style GetPointsHeaderStyle(DependencyObject element)
        {
            return (Style)element.GetValue(PointsHeaderStyleProperty);
        }

        public static void SetPointsElementStyle(DependencyObject element, Style value)
        {
            element.SetValue(PointsElementStyleProperty, value);
        }

        public static Style GetPointsElementStyle(DependencyObject element)
        {
            return (Style)element.GetValue(PointsElementStyleProperty);
        }

        public static void SetTimeElementStyle(DependencyObject element, Style value)
        {
            element.SetValue(TimeElementStyleProperty, value);
        }

        public static Style GetTimeElementStyle(DependencyObject element)
        {
            return (Style)element.GetValue(TimeElementStyleProperty);
        }

        public static void SetTimeDigits(DependencyObject element, int value)
        {
            element.SetValue(TimeDigitsProperty, value);
        }

        public static int GetTimeDigits(DependencyObject element)
        {
            return (int)element.GetValue(TimeDigitsProperty);
        }

        public static void SetTimeFormatter(DependencyObject element, IValueConverter value)
        {
            element.SetValue(TimeFormatterProperty, value);
        }

        public static IValueConverter GetTimeFormatter(DependencyObject element)
        {
            return (IValueConverter)element.GetValue(TimeFormatterProperty);
        }

        private static void OnRoundPointsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            if (dataGrid == null || e.NewValue == null)
                return;

            var newObservable = e.NewValue as INotifyCollectionChanged;
            var newItems = ((ICollection)e.NewValue).Cast<RaceLapPoints>();

            if (newObservable != null)
            {
                newObservable.CollectionChanged += (s, ev) => OnRoundPointsCollectionChanged(dataGrid, newItems, ev);
                ResetColumns(dataGrid, newItems);
            }
        }

        private static void OnRoundPointsCollectionChanged(DataGrid dataGrid, IEnumerable<RaceLapPoints> list, NotifyCollectionChangedEventArgs e)
        {
            var startColumn = GetRoundPointsStartColumn(dataGrid);
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    for (var i = 0; i < e.NewItems.Count; i++)
                    {
                        dataGrid.Columns.Insert(startColumn + (e.NewStartingIndex + i) * 2, dataGrid.CreateRoundTimeColumn((RaceLapPoints)e.NewItems[i]));
                        dataGrid.Columns.Insert(startColumn + (e.NewStartingIndex + i) * 2 + 1, dataGrid.CreateRoundPointsColumn((RaceLapPoints)e.NewItems[i]));
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    for (var i = 0; i < e.OldItems.Count; i++)
                    {
                        dataGrid.Columns.RemoveAt(startColumn + (e.OldStartingIndex + i) * 2 + 1);
                        dataGrid.Columns.RemoveAt(startColumn + (e.OldStartingIndex + i) * 2);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    dataGrid.Columns[startColumn + e.OldStartingIndex * 2] = dataGrid.CreateRoundTimeColumn((RaceLapPoints)e.NewItems[0]);
                    dataGrid.Columns[startColumn + e.OldStartingIndex * 2 + 1] = dataGrid.CreateRoundPointsColumn((RaceLapPoints)e.NewItems[0]);
                    break;

                default:
                    ResetColumns(dataGrid, list);
                    break;
            }
        }

        private static void ResetColumns(DataGrid dataGrid, IEnumerable<RaceLapPoints> roundPoints)
        {
            var startColumn = GetRoundPointsStartColumn(dataGrid);
            while (dataGrid.Columns.Count > startColumn)
                dataGrid.Columns.RemoveAt(startColumn);

            foreach (var round in roundPoints)
            {
                dataGrid.Columns.Add(dataGrid.CreateRoundTimeColumn(round));
                dataGrid.Columns.Add(dataGrid.CreateRoundPointsColumn(round));
            }
            dataGrid.Columns.Add(dataGrid.CreateTotalColumn());
            dataGrid.Columns.Add(dataGrid.CreateTimeInfoColumn());
        }

        private static DataGridTextColumn CreateRoundTimeColumn(this DataGrid dataGrid, RaceLapPoints raceLapPoints)
        {
            return new DataGridTextColumn
            {
                Header = raceLapPoints.Lap.RoundsToGo.ToString("#.#"),
                Binding = new Binding($"Laps.Points[{raceLapPoints.Lap.Index}].Time")
                {
                    Converter = GetTimeFormatter(dataGrid),
                    ConverterParameter = GetTimeDigits(dataGrid)
                },
                HeaderStyle = GetPointsHeaderStyle(dataGrid),
                ElementStyle = GetTimeElementStyle(dataGrid),
                Width = new DataGridLength(70)
            };
        }

        private static DataGridTextColumn CreateRoundPointsColumn(this DataGrid dataGrid, RaceLapPoints raceLapPoints)
        {
            return new DataGridTextColumn
            {
                Header = raceLapPoints.Type,
                Binding = new Binding($"Laps.Points[{raceLapPoints.Lap.Index}].Points")
                {
                    StringFormat = "#.#"
                },
                HeaderStyle = GetPointsHeaderStyle(dataGrid),
                ElementStyle = GetPointsElementStyle(dataGrid),
                Width = new DataGridLength(50)
            };
        }

        private static DataGridTextColumn CreateTotalColumn(this DataGrid dataGrid)
        {
            return new DataGridTextColumn
            {
                Header = "Total",
                Binding = new Binding("Laps.Points.Total")
                {
                    StringFormat = "#.#"
                },
                HeaderStyle = GetPointsHeaderStyle(dataGrid),
                ElementStyle = GetPointsElementStyle(dataGrid),
                Width = new DataGridLength(50)
            };
        }

        private static DataGridTemplateColumn CreateTimeInfoColumn(this DataGrid dataGrid)
        {
            return new DataGridTemplateColumn
            {
                CellTemplate = GetTimeInfoCellTemplate(dataGrid)
            };
        }

        public static void SetTimeInfoCellTemplate(DependencyObject element, DataTemplate value)
        {
            element.SetValue(TimeInfoCellTemplateProperty, value);
        }

        public static DataTemplate GetTimeInfoCellTemplate(DependencyObject element)
        {
            return (DataTemplate)element.GetValue(TimeInfoCellTemplateProperty);
        }
    }
}