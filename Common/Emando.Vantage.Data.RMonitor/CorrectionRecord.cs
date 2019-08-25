using System;
using System.Globalization;

namespace Emando.Vantage.Data.RMonitor
{
    public class CorrectionRecord : RMonitorRecord
    {
        public CorrectionRecord(string registrationNumber, string number, int lap, TimeSpan time, DateTime received)
            : base(received)
        {
            RegistrationNumber = registrationNumber;
            Number = number;
            Lap = lap;
            Time = time;
        }

        public string RegistrationNumber { get; private set; }

        public string Number { get; private set; }

        public int Lap { get; private set; }

        public TimeSpan Time { get; private set; }

        public static CorrectionRecord Parse(string[] fields, DateTime received)
        {
            string registrationNumber = fields[0];
            string number = fields[1];
            int lap = Convert.ToInt32(fields[2]);
            var time = TimeSpan.ParseExact(fields[3], "g", CultureInfo.GetCultureInfo("en-US"));
            return new CorrectionRecord(registrationNumber, number, lap, time, received);
        }
    }
}