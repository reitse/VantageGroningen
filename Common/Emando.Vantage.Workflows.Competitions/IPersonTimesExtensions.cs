using System;
using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public static class HistoricalTimeExtensions
    {
        public static IPersonLicenseTime PersonalBest(this ICollection<IPersonLicenseTime> source, string licenseIssuerId, string licenseDiscipline, string licenseKey,
            string distanceDiscipline, int distance)
        {
            return (from t in source
                    where t.LicenseIssuerId == licenseIssuerId && t.LicenseDiscipline == licenseDiscipline && t.LicenseKey == licenseKey
                        && t.DistanceDiscipline == distanceDiscipline && t.Distance == distance
                    select t).FirstOrDefault();
        }

        public static IEnumerable<IPersonLicenseTime> OnlyPersonalBestsBefore(this ICollection<IPersonLicenseTime> source, DateTime reference)
        {
            return from t in source
                   where t.Date >= reference || Equals(t, source.PersonalBest(t.LicenseIssuerId, t.LicenseDiscipline, t.LicenseKey, t.DistanceDiscipline, t.Distance))
                   select t;
        }
    }
}