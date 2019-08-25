using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace Emando.Vantage.Windows.Controls
{
    public static class ItemsControlExtensions
    {
        public static void ScrollToCenterOfView(this ItemsControl itemsControl, object item)
        {
            if (!itemsControl.TryScrollToCenterOfView(item))
            {
                var listBox = itemsControl as ListBox;
                if (listBox != null)
                    listBox.ScrollIntoView(item);

                itemsControl.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() => itemsControl.TryScrollToCenterOfView(item)));
            }
        }

        private static bool TryScrollToCenterOfView(this ItemsControl itemsControl, object item)
        {
            var container = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as UIElement;
            if (container == null)
                return false;

            ScrollContentPresenter presenter = null;
            for (Visual vis = container; vis != null && !Equals(vis, itemsControl); vis = VisualTreeHelper.GetParent(vis) as Visual)
                if ((presenter = vis as ScrollContentPresenter) != null)
                    break;

            if (presenter == null)
                return false;

            var scrollInfo = !presenter.CanContentScroll
                ? presenter
                : presenter.Content as IScrollInfo ?? FirstVisualChild(presenter.Content as ItemsPresenter) as IScrollInfo ?? presenter;

            var size = container.RenderSize;
            var center = container.TransformToAncestor((Visual)scrollInfo).Transform(new Point(size.Width / 2, size.Height / 2));
            center.Y += scrollInfo.VerticalOffset;
            center.X += scrollInfo.HorizontalOffset;

            if (scrollInfo is StackPanel || scrollInfo is VirtualizingStackPanel)
            {
                var logicalCenter = itemsControl.ItemContainerGenerator.IndexFromContainer(container) + 0.5;
                var orientation = scrollInfo is StackPanel ? ((StackPanel)scrollInfo).Orientation : ((VirtualizingStackPanel)scrollInfo).Orientation;
                if (orientation == Orientation.Horizontal)
                    center.X = logicalCenter;
                else
                    center.Y = logicalCenter;
            }

            if (scrollInfo.CanVerticallyScroll)
                scrollInfo.SetVerticalOffset(CenteringOffset(center.Y, scrollInfo.ViewportHeight, scrollInfo.ExtentHeight));
            if (scrollInfo.CanHorizontallyScroll)
                scrollInfo.SetHorizontalOffset(CenteringOffset(center.X, scrollInfo.ViewportWidth, scrollInfo.ExtentWidth));
            return true;
        }

        private static double CenteringOffset(double center, double viewport, double extent)
        {
            return Math.Min(extent - viewport, Math.Max(0, center - viewport / 2));
        }

        private static DependencyObject FirstVisualChild(DependencyObject visual)
        {
            if (visual == null)
                return null;
            if (VisualTreeHelper.GetChildrenCount(visual) == 0)
                return null;
            return VisualTreeHelper.GetChild(visual, 0);
        }
    }
}