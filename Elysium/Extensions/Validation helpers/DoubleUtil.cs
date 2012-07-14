using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Windows;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [UsedImplicitly]
    internal static class DoubleUtil
    {
        [UsedImplicitly]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        internal static bool IsNonNegative(double value)
        {
            return !double.IsNaN(value) && !double.IsInfinity(value) && value > 0d;
        }

        [DebuggerHidden]
        [UsedImplicitly]
        [ContractAbbreviator]
        internal static void EnsureNonNegative()
        {
            Contract.Ensures(IsNonNegative(Contract.Result<double>()));
        }

        [UsedImplicitly]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        internal static object CoerceNonNegative(DependencyObject obj, object basevalue)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var value = BoxingHelper<double>.Unbox(basevalue);
            return IsNonNegative(value) ? value : 0d;
        }
    }
} ;