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
            double param = double.Parse(parameter.ToString()!);

            if (value is double width && width > 0)
            {
                Console.WriteLine(width);
                return width / param; 
            }
            return 16; // 기본값 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
