using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using JetBrains.Annotations;

namespace Elysium.Converters
{
    [PublicAPI]
    [ValueConversion(typeof(Int16), typeof(Int16))]
    [ValueConversion(typeof(Int32), typeof(Int32))]
    [ValueConversion(typeof(Int64), typeof(Int64))]
    [ValueConversion(typeof(Single), typeof(Single))]
    [ValueConversion(typeof(Double), typeof(Double))]
    [ValueConversion(typeof(Decimal), typeof(Decimal))]
    public sealed class PercentToAngleConverter : IValueConverter
    {
        [PublicAPI]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Int16)
                return (Int16)((Int16)value * 360);
            if (value is Int32)
                return (Int32)value * 360;
            if (value is Int64)
                return (Int64)value * 360L;
            if (value is Single)
                return (Single)value * 360f;
            if (value is Double)
                return (Double)value * 360d;
            if (value is Decimal)
                return (Decimal)value * 360m;
            Trace.TraceError("Value must be a number.");
            return DependencyProperty.UnsetValue;
        }

        [PublicAPI]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Int16)
                return (Int16)((Int16)value * (1d / 360d));
            if (value is Int32)
                return (Int32)((Int32)value * (1d / 360d));
            if (value is Int64)
                return (Int64)((Int64)value * (1d / 360d));
            if (value is Single)
                return (Single)value * (1f / 360f);
            if (value is Double)
                return (Double)value * (1d / 360d);
            if (value is Decimal)
                return (Decimal)value * (1m / 360m);
            Trace.TraceError("Value must be a number.");
            return DependencyProperty.UnsetValue;
        }
    }
} ;