using System.Diagnostics.CodeAnalysis;
using System.Windows;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class MenuItem
    {
        [PublicAPI]
        public static readonly DependencyProperty BulletSizeProperty =
            DependencyProperty.RegisterAttached("BulletSize", typeof(double), typeof(MenuItem),
                                                new FrameworkPropertyMetadata(12d, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.MenuItem))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetBulletSize([NotNull] System.Windows.Controls.MenuItem obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(BulletSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetBulletSize([NotNull] System.Windows.Controls.MenuItem obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(BulletSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ArrowSizeProperty =
            DependencyProperty.RegisterAttached("ArrowSize", typeof(double), typeof(MenuItem),
                                                new FrameworkPropertyMetadata(8d, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.MenuItem))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetArrowSize([NotNull] System.Windows.Controls.MenuItem obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(ArrowSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetArrowSize([NotNull] System.Windows.Controls.MenuItem obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ArrowSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ArrowMarginProperty =
            DependencyProperty.RegisterAttached("ArrowMargin", typeof(Thickness), typeof(MenuItem),
                                                new FrameworkPropertyMetadata(new Thickness(3d, 0d, 3d, 0d), FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.MenuItem))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Thickness GetArrowMargin([NotNull] System.Windows.Controls.MenuItem obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            ThicknessUtil.EnsureNonNegative();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(ArrowMarginProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetArrowMargin([NotNull] System.Windows.Controls.MenuItem obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ArrowMarginProperty, value);
        }
    }
} ;