using System.Windows;
using System.Windows.Media;

namespace Emando.Vantage.Windows.Controls
{
    internal static class DependencyObjectExtensions
    {
        public static T FindAncestor<T>(this DependencyObject obj) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                if (parent is T)
                    return (T)parent;

                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }
    }
}