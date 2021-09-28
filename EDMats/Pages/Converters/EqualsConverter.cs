using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using static System.Convert;

namespace EDMats.Pages.Converters
{
    public class EqualsConverter : MarkupExtension, IValueConverter
    {
        public object ValueWhenEqual { get; set; }

        public object ValueWhenNotEqual { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ChangeType(
                Equals(value, ChangeType(parameter, value?.GetType() ?? typeof(object), culture))
                    ? ValueWhenEqual
                    : ValueWhenNotEqual,
                targetType,
                culture
            );

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();

        public override object ProvideValue(IServiceProvider serviceProvider)
            => this;
    }
}