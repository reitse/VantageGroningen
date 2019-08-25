using System;
using System.Threading;

namespace Emando.Vantage.Components
{
    public static class ThreadRandom
    {
        [ThreadStatic]
        private static Random threadInstance;

        public static Random Instance => threadInstance ?? (threadInstance = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId)));
    }
}