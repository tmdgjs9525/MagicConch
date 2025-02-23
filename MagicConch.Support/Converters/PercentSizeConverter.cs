using System;
using System.Globalization;
using System.Windows.Data;

namespace MagicConch.Support.Converters
{
    public class PercentSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double param = double.Parse(parameter.ToString()!);

            if (param == 0)
            {
                return 0;
            }

            if (value is double length && length > 0)
            {
                return length / param; 
            }
            return 16; // 기본값 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
