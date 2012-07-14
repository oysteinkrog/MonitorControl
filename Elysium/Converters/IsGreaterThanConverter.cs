using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using JetBrains.Annotations;

namespace Elysium.Converters
{
    [PublicAPI]
    [ValueConversion(typeof(Byte), typeof(Boolean))]
    [ValueConversion(typeof(Int16), typeof(Boolean))]
    [ValueConversion(typeof(Int32), typeof(Boolean))]
    [ValueConversion(typeof(Int64), typeof(Boolean))]
    [ValueConversion(typeof(Single), typeof(Boolean))]
    [ValueConversion(typeof(Double), typeof(Boolean))]
    [ValueConversion(typeof(Decimal), typeof(Boolean))]
    public sealed class IsGreaterThanConverter : IValueConverter
    {
        [PublicAPI]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Byte) && !(value is Int16) && !(value is Int32) && !(value is Int64) &&
                !(value is Single) && !(value is Double) && !(value is Decimal))
            {
                Trace.TraceError("Value must be a number.");
                return DependencyProperty.UnsetValue;
            }

            var number = (double)value;
            double comparand;

            var argument = parameter as string;
            if (argument != null)
            {
                var successfulConverted = double.TryParse(argument, out comparand);
                if (successfulConverted)
                {
                    return number < comparand;
                }

                Trace.TraceError("Invalid parameter. Parameter must be a number.");
                return DependencyProperty.UnsetValue;
            }

            if ((parameter is Byte) || (parameter is Int16) || (parameter is Int32) || (parameter is Int64) ||
                (parameter is Single) || (parameter is Double) || (parameter is Decimal))
            {
                comparand = (double)parameter;
                return number < comparand;
            }

            Trace.TraceError("Invalid parameter. Parameter must be a number.");
            return DependencyProperty.UnsetValue;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Contract.Ensures(false);
            throw new NotSupportedException();
        }
    }
} ;