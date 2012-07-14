using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using JetBrains.Annotations;

namespace Elysium.Converters
{
    [PublicAPI]
    [ValueConversion(typeof(Byte), typeof(Byte))]
    [ValueConversion(typeof(Int16), typeof(Int16))]
    [ValueConversion(typeof(Int32), typeof(Int32))]
    [ValueConversion(typeof(Int64), typeof(Int64))]
    [ValueConversion(typeof(Single), typeof(Single))]
    [ValueConversion(typeof(Double), typeof(Double))]
    [ValueConversion(typeof(Decimal), typeof(Decimal))]
    public sealed class NumberPositiveToNegativeConverter : IValueConverter
    {
        [PublicAPI]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Byte)
                return (Byte)((Byte)value * -1);
            if (value is Int16)
                return (Int16)((Int16)value * -1);
            if (value is Int32)
                return (Int32)value * -1;
            if (value is Int64)
                return (Int64)value * -1L;
            if (value is Single)
                return (Single)value * -1f;
            if (value is Double)
                return (Double)value * -1d;
            if (value is Decimal)
                return (Decimal)value * -1m;
            return DependencyProperty.UnsetValue;
        }

        [PublicAPI]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
} ;