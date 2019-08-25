using System.Windows;

namespace Emando.Vantage.Windows.Controls
{
    public interface IDropTarget
    {
        bool CanDrop(string target, string format, object data);

        void Drop(string target, string format, object data, Point relativePosition);
    }
}