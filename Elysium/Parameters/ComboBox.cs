using System.Diagnostics.CodeAnalysis;
using System.Windows;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class ComboBox
    {
        [PublicAPI]
        public static readonly DependencyProperty ButtonSizeProperty =
            DependencyProperty.RegisterAttached("ButtonSize", typeof(double), typeof(ComboBox),
                                                new FrameworkPropertyMetadata(18d, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.ComboBox))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetButtonSize([NotNull] System.Windows.Controls.ComboBox obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(ButtonSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetButtonSize([NotNull] System.Windows.Controls.ComboBox obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ButtonSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ArrowSizeProperty =
            DependencyProperty.RegisterAttached("ArrowSize", typeof(double), typeof(ComboBox),
                                                new FrameworkPropertyMetadata(8d, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.ComboBox))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetArrowSize([NotNull] System.Windows.Controls.ComboBox obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(ArrowSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetArrowSize([NotNull] System.Windows.Controls.ComboBox obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ArrowSizeProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty ArrowMarginProperty =
            DependencyProperty.RegisterAttached("ArrowMargin", typeof(Thickness), typeof(ComboBox),
                                                new FrameworkPropertyMetadata(new Thickness(5d, 0d, 5d, 0d), FrameworkPropertyMetadataOptions.AffectsArrange));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.ComboBox))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Thickness GetArrowMargin([NotNull] System.Windows.Controls.ComboBox obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            ThicknessUtil.EnsureNonNegative();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(ArrowMarginProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetArrowMargin([NotNull] System.Windows.Controls.ComboBox obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ArrowMarginProperty, value);
        }
    }
} ;