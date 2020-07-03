using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trading.Class
{
    public class Utility
    {
        static public DateTime GetLocalDateTime()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone);
            return localTime;
        }
    }
}
