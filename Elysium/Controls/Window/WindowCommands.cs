using System.Windows.Input;

using JetBrains.Annotations;

namespace Elysium.Controls
{
    [PublicAPI]
    public static class WindowCommands
    {
        [PublicAPI]
        public static RoutedUICommand Minimize
        {
            get { return _minimize ?? (_minimize = new RoutedUICommand("Minmize", "Minimize", typeof(Window))); }
        }

        private static RoutedUICommand _minimize;

        [PublicAPI]
        public static RoutedUICommand Maximize
        {
            get { return _maximize ?? (_maximize = new RoutedUICommand("Maximize", "Miaximize", typeof(Window))); }
        }

        private static RoutedUICommand _maximize;

        [PublicAPI]
        public static RoutedUICommand Restore
        {
            get { return _restore ?? (_restore = new RoutedUICommand("Restore", "Restore", typeof(Window))); }
        }

        private static RoutedUICommand _restore;

        [PublicAPI]
        public static RoutedUICommand Close
        {
            get { return _close ?? (_close = new RoutedUICommand("Close", "Close", typeof(Window))); }
        }

        private static RoutedUICommand _close;
    }
} ;