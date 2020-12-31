using System;
using System.Globalization;
using System.Windows.Data;

namespace EDMats.Converters
{
    public class StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new ArgumentException($"Expected 'String' target type, '{targetType.Name}' provided.", nameof(targetType));

            return (string)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}