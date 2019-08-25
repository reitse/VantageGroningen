using System.Windows;
using System.Windows.Controls;

namespace Emando.Vantage.Windows.Controls
{
    public class Bucket : ContentControl
    {
        static Bucket()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Bucket), new FrameworkPropertyMetadata(typeof(Bucket)));
        }
    }
}