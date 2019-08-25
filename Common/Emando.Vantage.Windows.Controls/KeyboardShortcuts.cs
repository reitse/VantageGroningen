using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Emando.Vantage.Windows.Controls
{
    public class KeyboardShortcuts : Behavior<Window>
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof(object), typeof(KeyboardShortcuts), new PropertyMetadata(default(object), OnSourceChanged));
        public object Source
        {
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((KeyboardShortcuts)d).OnSourceChanged(e.OldValue as IHaveKeyboardShortcuts, e.NewValue as IHaveKeyboardShortcuts);
        }

        private void OnSourceChanged(IHaveKeyboardShortcuts oldValue, IHaveKeyboardShortcuts newValue)
        {
            if (oldValue != null)
                foreach (var binding in (from s in oldValue.Shortcuts
                                         from ib in AssociatedObject.InputBindings.OfType<InputBinding>()
                                         let gesture = ib.Gesture as KeyGesture
                                         where gesture != null && gesture.Key == s.Key && gesture.Modifiers == s.Modifiers
                                         select ib).ToList())
                    AssociatedObject.InputBindings.Remove(binding);

            if (newValue == null)
                return;

            foreach (var shortcut in newValue.Shortcuts)
                AssociatedObject.InputBindings.Add(new KeyBinding(new RelayCommand(shortcut.Action), shortcut.Key, shortcut.Modifiers));
        }
    }
}