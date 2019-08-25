using System;

namespace Emando.Vantage.Components
{
    public static class ExceptionExtensions
    {
        public static Exception InnerMost(this Exception e)
        {
            while (e.InnerException != null)
                e = e.InnerException;
            return e;
        }
    }
}