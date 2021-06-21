﻿using System;
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
        {
            var result = _AreEqual(value, parameter) ? ValueIfEqual : FallbackValue;
            return result is IConvertible ? System.Convert.ChangeType(result, targetType) : result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();

        public override object ProvideValue(IServiceProvider serviceProvider)
            => this;

        private static bool _AreEqual(object value, object parameter)
            => Equals(value, value is null ? parameter : System.Convert.ChangeType(parameter, value.GetType()));
    }
}