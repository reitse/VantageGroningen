using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Emando.Vantage.Windows.Controls
{
    public class ListBoxSelection : Behavior<Selector>
    {
        public static readonly DependencyProperty EnableDeselectProperty = DependencyProperty.Register("EnableDeselect", typeof(bool), typeof(ListBoxSelection),
            new PropertyMetadata(false));

        public bool EnableDeselect
        {
            get { return (bool)GetValue(EnableDeselectProperty); }
            set { SetValue(EnableDeselectProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown += MouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= MouseLeftButtonDown;
            base.OnDetaching();
        }

        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (EnableDeselect)
            {
                AssociatedObject.SelectedIndex = -1;
                e.Handled = true;
            }
        }
    }
}