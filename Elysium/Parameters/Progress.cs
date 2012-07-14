using System.Diagnostics.CodeAnalysis;
using System.Windows;

using Elysium.Controls.Primitives;
using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class Progress
    {
        [PublicAPI]
        public static readonly DependencyProperty BusyElementSizeProperty =
            DependencyProperty.RegisterAttached("BusyElementSize", typeof(double), typeof(Progress),
                                                new FrameworkPropertyMetadata(4d, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(ProgressBase))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetBusyElementSize([NotNull] ProgressBase obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(BusyElementSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetBusyElementSize([NotNull] ProgressBase obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(BusyElementSizeProperty, value);
        }
    }
} ;