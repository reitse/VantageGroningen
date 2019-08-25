using System;
using Emando.Vantage.Data.MylapsX2;

namespace Emando.Vantage.Components.Diagnostics.MylapsX2
{
    public class X2TransponderScan : ITransponderScan
    {
        private readonly X2PassingEvent scan;

        internal X2TransponderScan(X2PassingEvent scan)
        {
            this.scan = scan;
        }

        #region ITransponderScan Members

        public long LoopId => scan.Where;
        public DateTime When => scan.When;
        public TransponderKey Key => new TransponderKey("MYLAPS ProChip", (long)scan.What);

        #endregion
    }
}