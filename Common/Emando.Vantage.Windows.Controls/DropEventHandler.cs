using System;

namespace Emando.Vantage.Windows.Controls
{
    public delegate void DropEventHandler(object sender, DropEventArgs e);

    public class DropEventArgs : EventArgs
    {
        public DropEventArgs(string format, object data)
        {
            this.Format = format;
            this.Data = data;
        }

        public string Format { get; }

        public object Data { get; }
    }
}