using System.Collections.Generic;

namespace Emando.Vantage
{
    public interface IHaveTransponders
    {
        IEnumerable<TransponderKey> Transponders { get; }
    }
}