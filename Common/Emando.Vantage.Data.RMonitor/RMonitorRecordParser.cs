using System;
using System.Linq;

namespace Emando.Vantage.Data.RMonitor
{
    internal class RMonitorRecordParser
    {
        public static RMonitorRecord Parse(string record, DateTime received)
        {
            if (record == null)
                return null;

            var fields = record.Split(',');
            string command = fields[0];
            var parameters = fields.Skip(1).Select(f => f.Trim('"')).ToArray();

            switch (command)
            {
                case "$COR":
                    return CorrectionRecord.Parse(parameters, received);

                default:
                    return new RMonitorRecord(received);
            }
        }
    }
}