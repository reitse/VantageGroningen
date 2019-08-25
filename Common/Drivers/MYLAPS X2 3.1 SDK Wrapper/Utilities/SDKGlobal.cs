using System;
using System.Text;
using System.Net;

namespace MylapsSDK.Utilities
{

    public class SDKHelperFunctions
    {
        public const Double SPEED_SCALE_KMH = 3.6;
        public const Double SPEED_SCALE_MPH = 2.2369362920544;

        /** Macro that sets or resets a bit in a variable (depending on value). */
        public static uint SetOrClearBit(uint bitset, int bit, bool set)
        {
            if (set)
                return (uint) (bitset | (uint) (1 << bit));
            else
                return (uint) (bitset & ~(1 << bit));
        }

        /** check if a bit is set in a bit set */
        public static bool IsBitSet(uint bitset, int bit)
        {
            return (bitset & (1 << bit)) != 0;
        }

        /** Convert the internal RSSI value to dBm. */
        public static double ConvertRssi2Dbm(ushort rssi)
        {
            return (double)((Int16)rssi / 100.0);
        }

        /** Convert the internal noise value to dBm. */
        public static double ConvertNoise2Dbm(byte noise)
        {
            return (double)(-(noise / 2.0));
        }

        /** Convert the internal temperature value to Celsius. */
        public static double ConvertTemperature2Celcius(byte temp)
        {
            return (double)(temp / 10.0);
        }

        /** Convert the internal voltage value to Volt. */
        public static double ConvertVoltage2Volt(byte volt)
        {
            return (double)(volt / 10.0);
        }

        /** Convert the internal latitude/longitude value to latitude or longitude. */
        public static double ConvertLatLongInt2Double(int latlong)
        {
            return latlong * 1.0e-7;
        }

        /** Convert the internal latitude/longitude value to latitude or longitude. */
        public static int ConvertLatLongDouble2Int(double latlong)
        {
            return (int)(latlong * 1e7);
        }

        /** Convert the internal auxiliary A/D voltage value to Volt. */
        public static double ConvertAuxAd2Volt(int volt)
        {
            return (double)(volt / 11.087); // !< Convert the internal auxiliary A/D
            // voltage value to Volt.
        }

        /** Convert the voltage to an internal auxiliary D/A value. */
        public static byte ConvertAuxVolt2DAC(byte volt)
        {
            return (byte)(volt * 11.087); // !< Convert the voltage to an internal
            // auxiliary D/A value.
        }

        public static string ToHexString(byte[] array)
        {
            var result = new StringBuilder();

            foreach (byte v in array)
            {
                result.Append( string.Format("0x{0:X2} ", v) );
            }

            result.Remove(result.Length - 1, 1);

            return result.ToString();
        }

        public static DateTime TimestampToDateTime(long timestamp, DateTimeKind kind)
        {
            //create a new DateTime value based on the Unix Epoch
            var converted = new DateTime(1970, 1, 1, 0, 0, 0, 0, kind);

            //multiply timestamp with 10 ( uS(10) to nS(100) )
            //add the timestamp to the value
            var newDateTime = converted.AddTicks(timestamp * 10);

            //return the value in string format
            return newDateTime;
        }

        public static Int64 DateTimeToTimestamp(DateTime datetime)
        {
            var converted = new DateTime(1970, 1, 1, 0, 0, 0, 0, datetime.Kind);
            return datetime.Subtract(converted).Ticks / 10;
        }

        public static string IPToString(long longIP)
        {
            return new IPAddress(longIP).ToString();
        }

        public static IPAddress StringToIP(String address)
        {
            return IPAddress.Parse(address);
        }

        public static string MACToString(long mac, bool includeDash)
        {
            if (includeDash)
            {
                return string.Format("{0:X2}-{1:X2}-{2:X2}-{3:X2}-{4:X2}-{5:X2}", (byte)(mac >> 40), (byte)(mac >> 32), (byte)(mac >> 24), (byte)(mac >> 16), (byte)(mac >> 8), (byte)mac).ToUpper();
            }
            else
            {
                return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}{4:X2}{5:X2}", (byte)(mac >> 40), (byte)(mac >> 32), (byte)(mac >> 24), (byte)(mac >> 16), (byte)(mac >> 8), (byte)mac).ToUpper();
            }
        }

        public static string VersionToString(uint version, bool includeBuild)
        {
            if (includeBuild)
            {
                return string.Format("{0:d}.{1:d}.{2:d}", version >> 24, (version >> 16) & 0xFF, version & 0xFFFF);
            }
            else
            {
                return string.Format("{0:d}.{1:d}", version >> 24, (version >> 16) & 0xFF);
            }
        }

        public static double ConvertBeaconStrength2Dbm(byte strength)
        {
            if (strength == 0)
                return 0.0;
            
            return Math.Log10(strength)*100 - 260;
        }

        public static double ConvertTransponderNoise2Dbm(byte noise)
        {
            return (double)((noise / 2.45) - 97.0);
        }

        public static double ConvertTransponderVoltage2Volt(byte voltage)
        {
            return voltage/10.0;
        }

        public static double ConvertDecoderTemperature2Celcius(byte temp)
        {
            return temp/1.0;
        }

        public static double ConvertTransponderTemperature2Celcius(byte temp)
        {
            return temp / 1.0;
        }

        public static double ConvertLoopTriggerTemperature2Celcius(ushort temp)
        {
            return ((double)((short)temp / 10.0));
        }

        public static Int64 TruncateTime(Int64 timeInMicroSeconds, UInt32 nrOfDecimals)
        {
            Int64 result = 0,
                factor = 1;

            switch (nrOfDecimals)
            {
                case 0: factor = 1000000; break;
                case 1: factor = 100000; break;
                case 2: factor = 10000; break;
                case 3: factor = 1000; break;
                case 4: factor = 100; break;
                case 5: factor = 10; break;
            }

            result = timeInMicroSeconds / factor;
            return result * factor;
        }

        public static String GetTransponderAsString(UInt32 aTransponder, Byte aType)
        {
            String result = String.Empty;
            return result;
        }

        public static UInt32 StringToTransponder(String aTransponder)
        {
            UInt32 result = UInt32.MaxValue;
            return result;
        }

        public static String GetTimeAsString(UInt32 aTime, bool bAdjustFormat, int NrDecimals)
        {
            if (aTime != UInt32.MaxValue)
            {
                return GetTimeAsString((Int64)aTime, bAdjustFormat, NrDecimals);
            }
            else
            {
                return String.Empty;
            }
        }

        public static String GetTimeAsString(Int64 aTime, Boolean bAdjustFormat, Int32 NrDecimals)
        {
            String result = String.Empty;

	        if (aTime == MdpTime.InvalidTime) {
		        return result;
	        }

	        // Remove the date part from the mdp_time_t variable.
	        Int64 aDate = aTime / ((Int64)24 * 3600 * 1000000);
	        // Only Correct the Time if Days > 100.
	        if (aDate > 50) {
		        aTime -= aDate * ((Int64)24 * 3600 * 1000000);
            }
	        // Check the sign 'o' the times.
	        bool NegativeSign = false;
	        if (aTime < 0) {
		        NegativeSign = true;
		        aTime = -aTime;
	        }

	        Int32 Hour, Minute, Second, MicroSecond;
	        MicroSecond = (Int32)(aTime % 1000000);
	        Second = (Int32)((aTime / 1000000) % 60);
	        Minute = (Int32)((aTime / (60 * 1000000)) % 60);
	        Hour = (Int32)(aTime / ((Int64)60 * 60 * 1000000));

	        if (!bAdjustFormat || Hour != 0) {
                result = String.Format("{0}{1:d}:{2:d2}:{3:d2}", NegativeSign ? "-" : "", Hour, Minute, Second);
            }
	        else {
		        if (Minute != 0) {
                    result = String.Format("{0}{1:d}:{2:d2}", NegativeSign ? "-" : "", Minute, Second);
                }
		        else {
                    result = String.Format("{0}{1:d}", NegativeSign ? "-" : "", Second);
                }
	        }

	        // Add the decimals.
            String decimals = String.Empty;
	        switch (NrDecimals) {
                case 1: decimals = String.Format(".{0:d1}", MicroSecond / 100000); break;
                case 2: decimals = String.Format(".{0:d2}", MicroSecond / 10000); break;
                case 3: decimals = String.Format(".{0:d3}", MicroSecond / 1000); break;
                case 4: decimals = String.Format(".{0:d4}", MicroSecond / 100); break;
                case 5: decimals = String.Format(".{0:d5}", MicroSecond / 10); break;
                case 6: decimals = String.Format(".{0:d6}", MicroSecond); break;
	        }

            result += decimals;
            return result;
        }

        public static String GetSpeedAsString(Int64 aTime, Int64 Length, Double Scale, Int32 NrDecimals)
        {
            if (aTime == MdpTime.InvalidTime || aTime == 0)
            {
                return String.Empty;
            }

            Int64 Factor = 1;
            String format = "{0:f0.000000}";
            switch (NrDecimals)
            {
                case 0: Factor = 1000000; format = "{0:0}"; break;
                case 1: Factor = 100000; format = "{0:0.0}"; break;
                case 2: Factor = 10000; format = "{0:0.00}"; break;
                case 3: Factor = 1000; format = "{0:0.000}"; break;
                case 4: Factor = 100; format = "{0:0.0000}"; break;
                case 5: Factor = 10; format = "{0:0.00000}"; break;
            }
            // Calculate the speed (without losing resolution and truncate on the number-of-decimals)
            Double speed = (((Length * Factor) / aTime) / (Double)Factor) * Scale;

            return String.Format(format, speed);// Format the string.
        }

        public static String GetSpeedAsKmPerHourString(Int64 aTime, Int64 Length, Int32 NrDecimals)
        {
            return GetSpeedAsString(aTime, Length, SDKHelperFunctions.SPEED_SCALE_KMH, NrDecimals);
        }

        public static String GetSpeedAsMilesPerHourString(Int64 aTime, Int64 Length, Int32 NrDecimals)
        {
            return GetSpeedAsString(aTime, Length, SDKHelperFunctions.SPEED_SCALE_MPH, NrDecimals);
        }

        public static string UTF8ByteArrayToString(byte[] utf8data)
        {
            string str = System.Text.Encoding.UTF8.GetString(utf8data);
            Int32 end = str.IndexOf('\0');
            if (end == -1)
            {
                return str;
            }
            else
            {
                return str.Substring(0, end);
            }
        }

        public static void StringToUTF8ByteArray(string str, byte[] destByteArray)
        {
            Byte[] source = System.Text.Encoding.UTF8.GetBytes(str);
            Int32 length = source.Length < destByteArray.Length ? source.Length : destByteArray.Length-1;
            Array.Clear(destByteArray, 0, destByteArray.Length);
            Array.Copy(source, destByteArray, length);
        }
    }
}
