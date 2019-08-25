using System.Collections.Generic;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Components.Adapters.KNSB
{
    internal class TransponderKeyComparer : IEqualityComparer<Transponder>
    {
        public bool Equals(Transponder x, Transponder y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;
            return x.Type == y.Type && x.Code == y.Code;
        }

        public int GetHashCode(Transponder obj)
        {
            return obj != null ? obj.GetHashCode() : 0;
        }
    }
}