using System;

namespace MonitorControl.Model
{
    public static class OS
    {
        public static bool IsVistaOrLater
        {
            get { return Environment.OSVersion.Version.Major >= 6; }
        }

        public static bool Is64BitProcess
        {
            get { return (IntPtr.Size == 8); }
        }
    }
}