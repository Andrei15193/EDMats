using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using EDMats.Models.Trading;

namespace EDMats.Pages.Converters
{
    public class TradeSolutionBrushConverter : IValueConverter
    {
        public Brush NoTradeSolutionBrush { get; set; }

        public Brush EmptyTradeSolutionBrush { get; set; }

        public Brush TradeSolutionBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tradeSolution = (TradeSolution)value;
            if (tradeSolution is null)
                return NoTradeSolutionBrush;
            else if (tradeSolution.Trades.Count == 0)
                return EmptyTradeSolutionBrush;
            else
                return TradeSolutionBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}