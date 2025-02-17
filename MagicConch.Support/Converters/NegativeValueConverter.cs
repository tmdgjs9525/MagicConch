using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MagicConch.Support.Converters
{
    internal class NegativeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double number)
            {
                return -number;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
