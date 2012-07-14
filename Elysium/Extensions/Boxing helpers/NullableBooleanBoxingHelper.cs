using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [UsedImplicitly]
    internal static class NullableBooleanBoxingHelper
    {
        [UsedImplicitly]
        internal static object Box(bool? value)
        {
            return value.HasValue ? BooleanBoxingHelper.Box(value.Value) : null;
        }

        [UsedImplicitly]
        internal static bool? Unbox(object value)
        {
            return value == null ? new bool?() : BooleanBoxingHelper.Unbox(value);
        }
    }
} ;