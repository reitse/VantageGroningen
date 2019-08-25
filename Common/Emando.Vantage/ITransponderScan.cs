using System;

namespace Emando.Vantage
{
    public interface ITransponderScan
    {
        long LoopId { get; }
        DateTime When { get; }
        TransponderKey Key { get; }
    }
}