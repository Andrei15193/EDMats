using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using EDMats.Models.Trading;

namespace EDMats.Pages.Converters
{
    public class TradeSolutionTextDecorationsConverter : IValueConverter
    {
        public TextDecorationCollection NoTradeSolutionTextDecoration { get; set; } = new TextDecorationCollection();

        public TextDecorationCollection EmptyTradeSolutionTextDecoration { get; set; } = new TextDecorationCollection();

        public TextDecorationCollection TradeSolutionTextDecoration { get; set; } = new TextDecorationCollection();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tradeSolution = (TradeSolution)value;
            if (tradeSolution is null)
                return NoTradeSolutionTextDecoration;
            else if (tradeSolution.Trades.Count == 0)
                return EmptyTradeSolutionTextDecoration;
            else
                return TradeSolutionTextDecoration;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}