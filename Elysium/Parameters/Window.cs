using System.Diagnostics.CodeAnalysis;
using System.Windows;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Parameters
{
    [PublicAPI]
    public static class Window
    {
        [PublicAPI]
        public static readonly DependencyProperty ResizeBorderThicknessProperty =
            DependencyProperty.RegisterAttached("ResizeBorderThickness", typeof(Thickness), typeof(Window),
                                                new FrameworkPropertyMetadata(new Thickness(3d), FrameworkPropertyMetadataOptions.AffectsArrange,
                                                                              null, ThicknessUtil.CoerceNonNegative));

        [PublicAPI]
        [AttachedPropertyBrowsableForType(typeof(Controls.Window))]
        [SuppressMessage("Microsoft.Contracts", "Ensures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Thickness GetResizeBorderThickness([NotNull] Controls.Window obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            ThicknessUtil.EnsureNonNegative();
            return BoxingHelper<Thickness>.Unbox(obj.GetValue(ResizeBorderThicknessProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetResizeBorderThickness([NotNull] Controls.Window obj, Thickness value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(ResizeBorderThicknessProperty, value);
        }
    }
} ;