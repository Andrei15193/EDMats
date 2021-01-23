using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace EDMats.Converters
{
    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public Visibility VisibilityWhenTrue { get; set; } = Visibility.Visible;

        public Visibility VisibilityWhenFalse { get; set; } = Visibility.Collapsed;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool booleanValue && booleanValue ? VisibilityWhenTrue : VisibilityWhenFalse;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();

        public override object ProvideValue(IServiceProvider serviceProvider)
            => this;
    }
}