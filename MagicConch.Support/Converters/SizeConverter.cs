using System;
using System.Globalization;
using System.Windows.Data;

namespace MagicConch.Support.Converters
{
    public class SizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double param = double.Parse(parameter.ToString()!);

            if (value is double width && width > 0)
            {
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
