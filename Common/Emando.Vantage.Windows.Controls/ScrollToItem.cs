using System.Windows;
using System.Windows.Controls;

namespace Emando.Vantage.Windows.Controls
{
    public static class ScrollToItem
    {
        public static readonly DependencyProperty ItemProperty = DependencyProperty.RegisterAttached("Item", typeof(object), typeof(ScrollToItem),
            new PropertyMetadata(null, OnItemChanged));

        public static object GetItem(DependencyObject obj)
        {
            return obj.GetValue(ItemProperty);
        }

        public static void SetItem(DependencyObject obj, object value)
        {
            obj.SetValue(ItemProperty, value);
        }

        private static void OnItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ListBox)d).ScrollIntoView(e.NewValue);
        }
    }
}