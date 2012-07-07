using System;
using System.Windows;
using System.Windows.Interop;

namespace MonitorControl.Model
{
    public class MultiMonWindow
    {
        private readonly IntPtr _hwnd;
        private readonly Window _window;

        public MultiMonWindow(Window window)
        {
            this._window = window;
            this._hwnd = (new WindowInteropHelper(this._window)).Handle;
        }

        //        public Rectangle Position
        //        {
        //            get
        //            {
        //                return new Rectangle(_window.Left, _window.Top, _window.Width, _window.Height);
        //                var hMonitor = Win32.User32.MonitorFromWindow(_hwnd,Win32.User32.MonitorFromWindowFlags.MONITOR_DEFAULTTONEAREST);
        //            }
        //        }
        //
        //        public Monitor Monitor
        //        {
        //            get
        //            {
        //                var hMonitor = Win32.User32.MonitorFromWindow(_hwnd, Win32.User32.MonitorFromWindowFlags.MONITOR_DEFAULTTONEAREST);
        //            }
        //        }

        public override int GetHashCode()
        {
            return this._window.GetHashCode();
        }

        public void MoveToMonitor(Monitor monitor)
        {
            //            if (_window.IsLoaded)
            //            {
            //                _window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<Monitor>(MoveToMonitor), monitor);
            //                return;
            //            }
            this._window.Left = monitor.WorkArea.Left;
            this._window.Top = monitor.WorkArea.Top;
            this._window.Width = monitor.WorkArea.Width;
            this._window.Height = monitor.WorkArea.Height;

            //TODO: move monitor by win32 functions instead?
            //http://msdn.microsoft.com/en-us/library/dd162826(v=vs.85).aspx
        }
    }
}