// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace MonitorControl.Interop
{
    using System;
    using System.Runtime.InteropServices;

    public partial class Win32
    {
        public struct IDirect3DDevice9
        {
        }

        public static class Dxva2
        {
            public static class PhysicalMonitorEnumerationApi
            {
                #region Physical Monitor Constants

                /// <summary>
                /// A physical monitor description is always an array of 128 characters.  Some
                /// of the characters may not be used.
                /// </summary>
                public const int PHYSICAL_MONITOR_DESCRIPTION_SIZE = 128;

                #endregion

                #region Physical Monitor Structures

                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
                public struct PHYSICAL_MONITOR
                {
                    public IntPtr hPhysicalMonitor;

                    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PHYSICAL_MONITOR_DESCRIPTION_SIZE)]
                    public string szPhysicalMonitorDescription;
                }

                #endregion

                #region Physical Monitor Enumeration Functions

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, [Out] out UInt32 pdwNumberOfPhysicalMonitors);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern Win32.HRESULT GetNumberOfPhysicalMonitorsFromIDirect3DDevice9([In] ref IDirect3DDevice9 pDirect3DDevice9, [Out] out UInt32 pdwNumberOfPhysicalMonitors);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void GetPhysicalMonitorsFromHMONITOR([In] IntPtr hMonitor, [In] UInt32 dwPhysicalMonitorArraySize, [Out] PHYSICAL_MONITOR[] pPhysicalMonitorArray);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern Win32.HRESULT GetPhysicalMonitorsFromIDirect3DDevice9([In] ref IDirect3DDevice9 pDirect3DDevice9, [In] UInt32 dwPhysicalMonitorArraySize, [Out] PHYSICAL_MONITOR[] pPhysicalMonitorArray);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void DestroyPhysicalMonitor([In] IntPtr hMonitor);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void DestroyPhysicalMonitors([In] UInt32 dwPhysicalMonitorArraySize, [Out] PHYSICAL_MONITOR[] pPhysicalMonitorArray);

                #endregion
            }

            public static class HighlevelMonitorConfigurationApi
            {
                #region Flags

                [Flags]
                public enum MonitorCapabilities
                {
                     MC_CAPS_NONE = 0x00000000,
                     MC_CAPS_MONITOR_TECHNOLOGY_TYPE = 0x00000001,
                     MC_CAPS_BRIGHTNESS = 0x00000002,
                     MC_CAPS_CONTRAST = 0x00000004,
                     MC_CAPS_COLOR_TEMPERATURE = 0x00000008,
                     MC_CAPS_RED_GREEN_BLUE_GAIN = 0x00000010,
                     MC_CAPS_RED_GREEN_BLUE_DRIVE = 0x00000020,
                     MC_CAPS_DEGAUSS = 0x00000040,
                     MC_CAPS_DISPLAY_AREA_POSITION = 0x00000080,
                     MC_CAPS_DISPLAY_AREA_SIZE = 0x00000100,
                     MC_CAPS_RESTORE_FACTORY_DEFAULTS = 0x00000400,
                     MC_CAPS_RESTORE_FACTORY_COLOR_DEFAULTS = 0x00000800,
                     MC_RESTORE_FACTORY_DEFAULTS_ENABLES_MONITOR_SETTINGS = 0x00001000,
                }

                public const uint MC_CAPS_NONE = 0x00000000;
                public const uint MC_CAPS_MONITOR_TECHNOLOGY_TYPE = 0x00000001;
                public const uint MC_CAPS_BRIGHTNESS = 0x00000002;
                public const uint MC_CAPS_CONTRAST = 0x00000004;
                public const uint MC_CAPS_COLOR_TEMPERATURE = 0x00000008;
                public const uint MC_CAPS_RED_GREEN_BLUE_GAIN = 0x00000010;
                public const uint MC_CAPS_RED_GREEN_BLUE_DRIVE = 0x00000020;
                public const uint MC_CAPS_DEGAUSS = 0x00000040;
                public const uint MC_CAPS_DISPLAY_AREA_POSITION = 0x00000080;
                public const uint MC_CAPS_DISPLAY_AREA_SIZE = 0x00000100;
                public const uint MC_CAPS_RESTORE_FACTORY_DEFAULTS = 0x00000400;
                public const uint MC_CAPS_RESTORE_FACTORY_COLOR_DEFAULTS = 0x00000800;
                public const uint MC_RESTORE_FACTORY_DEFAULTS_ENABLES_MONITOR_SETTINGS = 0x00001000;


                [Flags]
                public enum MonitorSupportedColorTemperatures
                {
                    MC_SUPPORTED_COLOR_TEMPERATURE_NONE = 0x00000000,
                    MC_SUPPORTED_COLOR_TEMPERATURE_4000K = 0x00000001,
                    MC_SUPPORTED_COLOR_TEMPERATURE_5000K = 0x00000002,
                    MC_SUPPORTED_COLOR_TEMPERATURE_6500K = 0x00000004,
                    MC_SUPPORTED_COLOR_TEMPERATURE_7500K = 0x00000008,
                    MC_SUPPORTED_COLOR_TEMPERATURE_8200K = 0x00000010,
                    MC_SUPPORTED_COLOR_TEMPERATURE_9300K = 0x00000020,
                    MC_SUPPORTED_COLOR_TEMPERATURE_10000K = 0x00000040,
                    MC_SUPPORTED_COLOR_TEMPERATURE_11500K = 0x00000080,
                }

                public const uint MC_SUPPORTED_COLOR_TEMPERATURE_NONE = 0x00000000;
                public const uint MC_SUPPORTED_COLOR_TEMPERATURE_4000K = 0x00000001;
                public const uint MC_SUPPORTED_COLOR_TEMPERATURE_5000K = 0x00000002;
                public const uint MC_SUPPORTED_COLOR_TEMPERATURE_6500K = 0x00000004;
                public const uint MC_SUPPORTED_COLOR_TEMPERATURE_7500K = 0x00000008;
                public const uint MC_SUPPORTED_COLOR_TEMPERATURE_8200K = 0x00000010;
                public const uint MC_SUPPORTED_COLOR_TEMPERATURE_9300K = 0x00000020;
                public const uint MC_SUPPORTED_COLOR_TEMPERATURE_10000K = 0x00000040;
                public const uint MC_SUPPORTED_COLOR_TEMPERATURE_11500K = 0x00000080;

                #endregion

                #region Enumerations

                public enum MC_DISPLAY_TECHNOLOGY_TYPE
                {
                    MC_SHADOW_MASK_CATHODE_RAY_TUBE,
                    MC_APERTURE_GRILL_CATHODE_RAY_TUBE,
                    MC_THIN_FILM_TRANSISTOR,
                    MC_LIQUID_CRYSTAL_ON_SILICON,
                    MC_PLASMA,
                    MC_ORGANIC_LIGHT_EMITTING_DIODE,
                    MC_ELECTROLUMINESCENT,
                    MC_MICROELECTROMECHANICAL,
                    MC_FIELD_EMISSION_DEVICE
                }

                public enum MC_DRIVE_TYPE
                {
                    MC_RED_DRIVE,
                    MC_GREEN_DRIVE,
                    MC_BLUE_DRIVE
                }

                public enum MC_GAIN_TYPE
                {
                    MC_RED_GAIN,
                    MC_GREEN_GAIN,
                    MC_BLUE_GAIN
                }

                public enum MC_POSITION_TYPE
                {
                    MC_HORIZONTAL_POSITION,
                    MC_VERTICAL_POSITION
                }

                public enum MC_SIZE_TYPE
                {
                    MC_WIDTH,
                    MC_HEIGHT
                }

                [Flags]
                public enum MC_COLOR_TEMPERATURE
                {
                    MC_COLOR_TEMPERATURE_UNKNOWN,
                    MC_COLOR_TEMPERATURE_4000K,
                    MC_COLOR_TEMPERATURE_5000K,
                    MC_COLOR_TEMPERATURE_6500K,
                    MC_COLOR_TEMPERATURE_7500K,
                    MC_COLOR_TEMPERATURE_8200K,
                    MC_COLOR_TEMPERATURE_9300K,
                    MC_COLOR_TEMPERATURE_10000K,
                    MC_COLOR_TEMPERATURE_11500K
                };

                #endregion

                #region Monitor capability functions

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void GetMonitorCapabilities(IntPtr hMonitor, [Out] out MonitorCapabilities pdwMonitorCapabilities, [Out] out MonitorSupportedColorTemperatures pdwSupportedColorTemperatures);

                #endregion

                #region Monitor setting persistence functions

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void SaveCurrentMonitorSettings(IntPtr hMonitor);

                #endregion

                #region Monitor meta-data functions

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void GetMonitorTechnologyType(IntPtr hMonitor, ref MC_DISPLAY_TECHNOLOGY_TYPE pdtyDisplayTechnologyType);

                #endregion

                #region Monitor image calibration functions

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void GetMonitorBrightness(IntPtr hMonitor, [Out] out UInt32 pdwMinimumBrightness, [Out] out UInt32 pdwCurrentBrightness, [Out] out UInt32 pdwMaximumBrightness);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void GetMonitorContrast(IntPtr hMonitor, [Out] out UInt32 pdwMinimumContrast, [Out] out UInt32 pdwCurrentContrast, [Out] out UInt32 pdwMaximumContrast);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void GetMonitorColorTemperature(IntPtr hMonitor, [Out] out MC_COLOR_TEMPERATURE pctCurrentColorTemperature);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void GetMonitorRedGreenOrBlueDrive(IntPtr hMonitor, MC_DRIVE_TYPE dtDriveType, [Out] out UInt32 pdwMinimumDrive, [Out] out UInt32 pdwCurrentDrive, [Out] out UInt32 pdwMaximumDrive);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void GetMonitorRedGreenOrBlueGain(IntPtr hMonitor, MC_GAIN_TYPE gtGainType, [Out] out UInt32 pdwMinimumGain, [Out] out UInt32 pdwCurrentGain, [Out] out UInt32 pdwMaximumGain);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void SetMonitorBrightness(IntPtr hMonitor, UInt32 dwNewBrightness);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void SetMonitorContrast(IntPtr hMonitor, UInt32 dwNewContrast);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void SetMonitorColorTemperature(IntPtr hMonitor, MC_COLOR_TEMPERATURE ctCurrentColorTemperature);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void SetMonitorRedGreenOrBlueDrive(IntPtr hMonitor, MC_DRIVE_TYPE dtDriveType, UInt32 dwNewDrive);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void SetMonitorRedGreenOrBlueGain(IntPtr hMonitor, MC_GAIN_TYPE gtGainType, UInt32 dwNewGain);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void DegaussMonitor(IntPtr hMonitor);

                #endregion

                #region Monitor image size and position calibration functions

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void GetMonitorDisplayAreaSize(IntPtr hMonitor, MC_SIZE_TYPE stSizeType, [Out] out UInt32 pdwMinimumWidthOrHeight, [Out] out UInt32 pdwCurrentWidthOrHeight, [Out] out UInt32 pdwMaximumWidthOrHeight);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void GetMonitorDisplayAreaPosition(IntPtr hMonitor, MC_POSITION_TYPE ptPositionType, [Out] out UInt32 pdwMinimumPosition,
                                                                        [Out] out UInt32 pdwCurrentPosition, [Out] out UInt32 pdwMaximumPosition);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void SetMonitorDisplayAreaSize(IntPtr hMonitor, MC_SIZE_TYPE stSizeType, UInt32 dwNewDisplayAreaWidthOrHeight);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void SetMonitorDisplayAreaPosition(IntPtr hMonitor, MC_POSITION_TYPE ptPositionType, UInt32 dwNewPosition);

                #endregion

                #region Restore to defaults functions

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void RestoreMonitorFactoryColorDefaults(IntPtr hMonitor);

                [DllImport("Dxva2.dll", ExactSpelling = true, SetLastError = true, PreserveSig = false)]
                public static extern void RestoreMonitorFactoryDefaults(IntPtr hMonitor);

                #endregion
            }
        }
    }
}

// ReSharper restore InconsistentNaming
// ReSharper restore UnusedMember.Global
// ReSharper restore MemberCanBePrivate.Global
// ReSharper restore FieldCanBeMadeReadOnly.Global
// ReSharper restore ClassNeverInstantiated.Global