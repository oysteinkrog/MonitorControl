using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [PublicAPI]
    public static class ThemeManagerExtensions
    {
        [PublicAPI]
        public static Theme GetTheme(this FrameworkElement reference)
        {
            ValidationHelper.NotNull(reference, () => reference);

            Theme? theme;
            var frameworkElement = reference;
            do
            {
                theme = ThemeManager.GetTheme(frameworkElement);
                frameworkElement = LogicalTreeHelper.GetParent(reference) as FrameworkElement;
            }
            while (theme == null && frameworkElement != null);

            return theme ?? GetTheme(Application.Current);
        }

        [PublicAPI]
        public static Theme GetTheme(this Application reference)
        {
            ValidationHelper.NotNull(reference, () => reference);

            var lightBrushesUri = new Uri("/Elysium;component/Themes/LightBrushes.xaml", UriKind.Relative);
            var darkBrushesUri = new Uri("/Elysium;component/Themes/DarkBrushes.xaml", UriKind.Relative);
            var lightBrushesDictionaryExist = reference.Resources.MergedDictionaries.Any(dictionary => dictionary.Source == lightBrushesUri);
            var darkBrushesDictionaryExist = reference.Resources.MergedDictionaries.Any(dictionary => dictionary.Source == darkBrushesUri);

            if (lightBrushesDictionaryExist && darkBrushesDictionaryExist)
            {
                throw new InvalidOperationException();
            }
            if (lightBrushesDictionaryExist)
            {
                return Theme.Light;
            }
            if (darkBrushesDictionaryExist)
            {
                return Theme.Dark;
            }
            throw new InvalidOperationException();
        }

        [PublicAPI]
        public static SolidColorBrush GetAccentBrush(this FrameworkElement reference)
        {
            ValidationHelper.NotNull(reference, () => reference);

            SolidColorBrush accentBrush;
            var frameworkElement = reference;
            do
            {
                accentBrush = ThemeManager.GetAccentBrush(frameworkElement);
                frameworkElement = LogicalTreeHelper.GetParent(reference) as FrameworkElement;
            }
            while (accentBrush == null && frameworkElement != null);

            return accentBrush ?? GetAccentBrush(Application.Current);
        }

        [PublicAPI]
        public static SolidColorBrush GetAccentBrush(this Application reference)
        {
            ValidationHelper.NotNull(reference, () => reference);

            var accentBrushExist = reference.Resources.Contains("AccentBrush");
            if (!accentBrushExist)
            {
                throw new InvalidOperationException();
            }
            return (SolidColorBrush)reference.Resources["AccentBrush"];
        }

        [PublicAPI]
        public static SolidColorBrush GetContrastBrush(this FrameworkElement reference)
        {
            ValidationHelper.NotNull(reference, () => reference);

            SolidColorBrush contrastBrush;
            var frameworkElement = reference;
            do
            {
                contrastBrush = ThemeManager.GetContrastBrush(frameworkElement);
                frameworkElement = LogicalTreeHelper.GetParent(reference) as FrameworkElement;
            }
            while (contrastBrush == null && frameworkElement != null);

            return contrastBrush ?? GetContrastBrush(Application.Current);
        }

        [PublicAPI]
        public static SolidColorBrush GetContrastBrush(this Application reference)
        {
            ValidationHelper.NotNull(reference, () => reference);

            var contrastBrushExist = reference.Resources.Contains("ContrastBrush");
            if (!contrastBrushExist)
            {
                throw new InvalidOperationException();
            }
            return (SolidColorBrush)reference.Resources["ContrastBrush"];
        }
    }
} ;