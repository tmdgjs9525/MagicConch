using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MagicConch.Support.Converters
{
    public class PercentMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string param = parameter.ToString();

            Thickness margin = new Thickness(0);

            if (value is Thickness thickness)
            {
                double width  = Application.Current.MainWindow.ActualWidth;
                double height = Application.Current.MainWindow.ActualHeight;

                thickness.Left = width;

                switch (param)
                {
                    case "Left":
                        margin = new Thickness(width / (double)value, 0, 0, 0);
                        break;
                    case "Top":
                        margin = new Thickness(0, height / (double)value, 0, 0);
                        break;
                    case "Right":
                        margin = new Thickness(0, 0, width / (double)value, 0);
                        break;
                    case "Bottom":
                        margin = new Thickness(0, 0, 0, height / (double)value);
                        break;
                    default:
                        break;
                }
            }
            return margin; // 기본값 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
