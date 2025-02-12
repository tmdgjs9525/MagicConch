using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicConch.Extensions
{
    internal class TimeZoneInfoHelper
    {
        public static string GetTimeZoneAbbreviation(TimeZoneInfo timeZone)
        {
            switch (timeZone.Id)
            {
                case "Korea Standard Time": return "KST";
                case "Pacific Standard Time": return "PST";
                case "Eastern Standard Time": return "EST";
                case "Greenwich Standard Time": return "GMT";
                case "UTC": return "UTC";
                default: return timeZone.StandardName;
            }
        }
    }
}
