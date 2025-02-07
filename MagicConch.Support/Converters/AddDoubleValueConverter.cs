using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MagicConch.Support.Converters
{
    public class AddDoubleValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width && parameter is string param)
            {
                if (double.TryParse(param, out double addValue))
                {
                    return width + addValue;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // ConvertBack은 구현하지 않음
            throw new NotImplementedException();
        }
    }
}
