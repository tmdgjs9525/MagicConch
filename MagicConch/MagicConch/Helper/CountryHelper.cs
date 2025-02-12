using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicConch.Helper
{
    internal class CountryHelper
    {
        public static string GetCurrentCountryNameInEnglish()
        {
            // 시스템의 현재 국가 코드(ISO 3166-1 alpha-2) 가져오기

            try
            {
                var region = new RegionInfo(CultureInfo.InstalledUICulture.Name);
                return region.EnglishName; // 영어 이름 반환
            }
            catch (ArgumentException)
            {
                return "Unknown Country Code"; // 예외 처리
            }
        }
    }
}
