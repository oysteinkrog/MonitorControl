using System.Diagnostics.CodeAnalysis;
using System.Windows;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class CheckBox
    {
        [PublicAPI]
        public static readonly DependencyProperty CheckSizeProperty =
            DependencyProperty.RegisterAttached("CheckSize", typeof(double), typeof(CheckBox),
                                                new FrameworkPropertyMetadata(10d, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              null, DoubleUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.CheckBox))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static double GetCheckSize([NotNull] System.Windows.Controls.CheckBox obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            DoubleUtil.EnsureNonNegative();
            return BoxingHelper<double>.Unbox(obj.GetValue(CheckSizeProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetCheckSize([NotNull] System.Windows.Controls.CheckBox obj, double value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(CheckSizeProperty, value);
        }
    }
} ;