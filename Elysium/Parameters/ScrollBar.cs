using System.Diagnostics.CodeAnalysis;
using System.Windows;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class ScrollBar
    {
        [PublicAPI]
        public static readonly DependencyProperty ArrowSizeProperty =
            DependencyProperty.RegisterAttached("ArrowSize", typeof(double), typeof(ScrollBar),
                                                new FrameworkPropertyMetadata(6d, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.Primitives.ScrollBar))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetArrowSize([NotNull] System.Windows.Controls.Primitives.ScrollBar obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(ArrowSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetArrowSize([NotNull] System.Windows.Controls.Primitives.ScrollBar obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ArrowSizeProperty, value);
        }
    }
} ;