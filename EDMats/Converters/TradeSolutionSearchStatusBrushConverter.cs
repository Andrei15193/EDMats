using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using EDMats.ViewModels;

namespace EDMats.Converters
{
    public class TradeSolutionSearchStatusBrushConverter : IValueConverter
    {
        public Brush IdleBrush { get; set; }

        public Brush SearchingBrush { get; set; }

        public Brush SearchSucceededBrush { get; set; }

        public Brush SearchFailedBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!typeof(Brush).IsAssignableFrom(targetType))
                throw new ArgumentException($"Expected 'Brush' target type, '{targetType.Name}' provided.", nameof(targetType));

            switch (value as TradeSolutionSearchStatus?)
            {
                case TradeSolutionSearchStatus.Idle:
                    return IdleBrush;

                case TradeSolutionSearchStatus.Searching:
                    return SearchingBrush;

                case TradeSolutionSearchStatus.SearchSucceeded:
                    return SearchSucceededBrush;

                case TradeSolutionSearchStatus.SearchFailed:
                    return SearchFailedBrush;

                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}