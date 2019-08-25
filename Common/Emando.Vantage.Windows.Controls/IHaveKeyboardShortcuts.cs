using System.Collections.Generic;

namespace Emando.Vantage.Windows.Controls
{
    public interface IHaveKeyboardShortcuts
    {
        IEnumerable<KeyboardShortcutAction> Shortcuts { get; }
    }
}