using System;
using System.Windows.Input;

namespace Emando.Vantage.Windows.Controls
{
    public struct KeyboardShortcutAction
    {
        public KeyboardShortcutAction(Key key, ModifierKeys modifiers, Action<object> action) : this()
        {
            Key = key;
            Modifiers = modifiers;
            Action = action;
        }

        public Key Key { get; set; }

        public ModifierKeys Modifiers { get; set; }

        public Action<object> Action { get; set; }
    }
}