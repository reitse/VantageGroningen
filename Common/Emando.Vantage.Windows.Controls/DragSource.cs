using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Emando.Vantage.Windows.Controls
{
    public class DragSource : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty AllowedEffectsProperty = DependencyProperty.Register("AllowedEffects", typeof(DragDropEffects), typeof(DragSource),
            new PropertyMetadata(DragDropEffects.Move));

        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(string), typeof(DragSource), new PropertyMetadata(null));

        private Point? dragBegin;

        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        public DragDropEffects AllowedEffects
        {
            get { return (DragDropEffects)GetValue(AllowedEffectsProperty); }
            set { SetValue(AllowedEffectsProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewMouseLeftButtonUp += OnPreviewMouseLeftButtonUp;
            AssociatedObject.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
            AssociatedObject.MouseMove += OnMouseMove;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseLeftButtonUp -= OnPreviewMouseLeftButtonUp;
            AssociatedObject.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
            AssociatedObject.MouseMove -= OnMouseMove;
            base.OnDetaching();
        }

        private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            dragBegin = null;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragBegin = e.GetPosition(null);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (dragBegin == null || e.LeftButton != MouseButtonState.Pressed)
                return;

            var diff = e.GetPosition(null) - dragBegin.Value;
            if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                DependencyObject dragSource;
                DataObject data;

                var itemsControl = AssociatedObject as ListBox;
                if (itemsControl != null)
                {
                    var itemContainer = ((DependencyObject)e.OriginalSource).FindAncestor<ListBoxItem>();
                    if (itemContainer == null)
                        return;

                    var item = itemsControl.ItemContainerGenerator.ItemFromContainer(itemContainer);
                    if (item == null)
                        return;

                    dragSource = itemContainer;
                    data = new DataObject(Format ?? item.GetType().ToString(), item);
                }
                else
                {
                    dragSource = AssociatedObject;
                    data = new DataObject(Format, AssociatedObject.DataContext);
                }

                DragDrop.DoDragDrop(dragSource, data, AllowedEffects);
            }
        }
    }
}