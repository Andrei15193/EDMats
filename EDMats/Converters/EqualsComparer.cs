using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace EDMats.Converters
{
    public class EqualsComparer : MarkupExtension, IValueConverter
    {
        public object ValueIfEqual { get; set; }

        public object FallbackValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => System.Convert.ChangeType(Equals(value, parameter) ? ValueIfEqual : FallbackValue, targetType);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();

        public override object ProvideValue(IServiceProvider serviceProvider)
            => this;
    }
}