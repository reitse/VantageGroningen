using System;

namespace Emando.Vantage.Data.Competitions.SpeedSkating.LongTrack.Sara2.Test
{
    public class MylapsTransponderCodeConverter
    {
        public const string ProChipType = "MYLAPS ProChip";

        private const string ProChipKey = "CFGHKLNPRSTVWXZ";
        private const long ProChipMinimum = 0x6000000;

        #region ITransponderCodeConverter Members

        public bool SupportsType(string type)
        {
            return type == ProChipType;
        }

        public bool TryConvertLabel(string type, string label, out long code)
        {
            code = default(long);

            if (type != ProChipType)
                return false;

            if (label == null || label.Length < 7)
                return false;

            label = label.Replace("-", "");
            if (label.Length == 7)
                label = $"C{label}";

            int active1 = ProChipKey.IndexOf(label[0]);
            int active2 = ProChipKey.IndexOf(label[1]);
            int active3 = ProChipKey.IndexOf(label[2]);

            if (active1 == -1 || active2 == -1 || active3 == -1)
                return false;

            code = 100000 * (active1 * 225 + active2 * 15 + active3);
            code += int.Parse(label.Substring(3));
            code += ProChipMinimum;
            return true;
        }

        #endregion
    }
}