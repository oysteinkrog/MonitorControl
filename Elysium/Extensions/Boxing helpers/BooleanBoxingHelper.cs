using System.Diagnostics.Contracts;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [UsedImplicitly]
    internal static class BooleanBoxingHelper
    {
        internal static readonly object FalseBox = false;
        internal static readonly object TrueBox = true;

        [UsedImplicitly]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        internal static object Box(bool value)
        {
            Contract.Ensures(Contract.Result<object>() != null);
            return value ? TrueBox : FalseBox;
        }

        [UsedImplicitly]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        internal static bool Unbox(object value)
        {
            Contract.Assume(value is bool);
            return BoxingHelper<bool>.Unbox(value);
        }
    }
} ;