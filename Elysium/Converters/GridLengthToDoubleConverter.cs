using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using JetBrains.Annotations;

namespace Elysium.Converters
{
    [PublicAPI]
    [ValueConversion(typeof(GridLength), typeof(double))]
    public sealed class GridLengthToDoubleConverter : IValueConverter
    {
        [PublicAPI]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is GridLength))
            {
                return DependencyProperty.UnsetValue;
            }

            var length = (GridLength)value;
            var unitType = parameter as string;
            switch (unitType)
            {
                case "Auto":
                    return length.IsAuto ? length.Value : DependencyProperty.UnsetValue;
                case "*":
                    return length.IsStar ? length.Value : DependencyProperty.UnsetValue;
                default:
                    return length.IsAbsolute ? length.Value : DependencyProperty.UnsetValue;
            }
        }

        [PublicAPI]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double))
            {
                return new GridLength(0d, GridUnitType.Auto);
            }

            var unitType = parameter as string;

            switch (unitType)
            {
                case "Auto":
                    return new GridLength(0d, GridUnitType.Auto);
                case "*":
                    return new GridLength((double)value, GridUnitType.Star);
                default:
                    return new GridLength((double)value, GridUnitType.Pixel);
            }
        }
    }
} ;