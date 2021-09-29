using System;
using System.Globalization;
using System.Windows.Data;
using EDMats.Models.Trading;

namespace EDMats.Pages.Converters
{
    public class TradeSolutionViewAvailableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is TradeSolution tradeSolution && tradeSolution.Trades.Count > 0;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}