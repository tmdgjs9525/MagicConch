using MagicConch.Support.Themes.Units;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MagicConch.Converter
{
    internal class FontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double param = double.Parse(parameter.ToString());
            if (value is double width && width > 0)
            {
                return width / param; // 기준 크기(예: 50배율)
            }
            return 16; // 기본값 (웹의 1rem = 16px처럼)
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
