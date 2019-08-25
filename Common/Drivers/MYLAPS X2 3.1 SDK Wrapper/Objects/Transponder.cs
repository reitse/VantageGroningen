using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Utilities;

namespace MylapsSDK.Objects
{
    partial class Transponder
    {
        // Smallest ProChip transponder id.
        private const UInt32 MIN_PROCHIP = 0x6000000;
        // The ProChip key.
        private const String ProChipKey = "CFGHKLNPRSTVWXZ";

        public override String ToString()
        {
            String result = String.Format("%d", this.ID);;

            if (this.ID == UInt32.MaxValue || 
                this.ID == 0 ||
                (this.ID & 0x1FFFFFFF) >= Transponder.MIN_PROCHIP)
            {
                return result;
            }

            switch ((TRANSPONDERTYPE)this.GetTransponderType())
            {
                case TRANSPONDERTYPE.ttUnavailable:
                case TRANSPONDERTYPE.ttProChip:
                    // Function to to convert to a pro chip number.
                        UInt32 proChipId = this.ID & 0x1FFFFFFF;
                        UInt32 m;
                        Int32 i;
                        proChipId -= Transponder.MIN_PROCHIP;
                        m = proChipId / (100000);
                        int start = (m % 15 == 0) ? 2 : 1;

                        for (i = start; i >= 0; i--)
                        {
                            Int32 keyIndex = (Int32)(m % 15);
                            result += Transponder.ProChipKey[keyIndex];
                            m /= 15;
                        }

                        result += String.Format("-%05d", this.ID % 100000);
                        if (result[0] == 'C' && result[3] == '-')
                        {
                            result = result.Substring(1, result.Length - 1);
                        }
                        break;
            }

            return result;
        }
    }
}