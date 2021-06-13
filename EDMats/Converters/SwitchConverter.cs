﻿using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace EDMats.Converters
{
    public class SwitchConverter : IValueConverter
    {
        public IEnumerable Cases { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => Cases.OfType<SwitchCase>().Single(@case => Equals(@case.Value, value)).MappedValue;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class SwitchCase
    {
        public object Value { get; set; }

        public object MappedValue { get; set; }
    }
}