using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace L4D2DevTools.Converters
{
    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string)value;
            if (!string.IsNullOrEmpty(path))
                return new BitmapImage(new Uri("pack://application:,,,/L4D2DevTools;component" + path));
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
