using System.Diagnostics.Contracts;
using System.Windows.Media;

using JetBrains.Annotations;

namespace Elysium
{
    [PublicAPI]
    public static class AccentBrushes
    {
        [PublicAPI]
        public static SolidColorBrush Blue
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_blue == null)
                {
                    _blue = new SolidColorBrush(Color.FromArgb(0xFF, 0x01, 0x7B, 0xCD));
                    _blue.Freeze();
                }
                return _blue;
            }
        }

        private static SolidColorBrush _blue;

        [PublicAPI]
        public static SolidColorBrush Brown
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_brown == null)
                {
                    _brown = new SolidColorBrush(Color.FromArgb(0xFF, 0xA0, 0x50, 0x00));
                    _brown.Freeze();
                }
                return _brown;
            }
        }

        private static SolidColorBrush _brown;

        [PublicAPI]
        public static SolidColorBrush Green
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_green == null)
                {
                    _green = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x99, 0x33));
                    _green.Freeze();
                }
                return _green;
            }
        }

        private static SolidColorBrush _green;

        [PublicAPI]
        public static SolidColorBrush Lime
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_lime == null)
                {
                    _lime = new SolidColorBrush(Color.FromArgb(0xFF, 0x8C, 0xBF, 0x26));
                    _lime.Freeze();
                }
                return _lime;
            }
        }

        private static SolidColorBrush _lime;

        [PublicAPI]
        public static SolidColorBrush Magenta
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_magenta == null)
                {
                    _magenta = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x00, 0x97));
                    _magenta.Freeze();
                }
                return _magenta;
            }
        }

        private static SolidColorBrush _magenta;

        [PublicAPI]
        public static SolidColorBrush Mango
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_mango == null)
                {
                    _mango = new SolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0x96, 0x09));
                    _mango.Freeze();
                }
                return _mango;
            }
        }

        private static SolidColorBrush _mango;

        [PublicAPI]
        public static SolidColorBrush Orange
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_orange == null)
                {
                    _orange = new SolidColorBrush(Color.FromArgb(0xFF, 0xCB, 0x52, 0x01));
                    _orange.Freeze();
                }
                return _orange;
            }
        }

        private static SolidColorBrush _orange;

        [PublicAPI]
        public static SolidColorBrush Pink
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_pink == null)
                {
                    _pink = new SolidColorBrush(Color.FromArgb(0xFF, 0xE6, 0x71, 0xB8));
                    _pink.Freeze();
                }
                return _pink;
            }
        }

        private static SolidColorBrush _pink;

        [PublicAPI]
        public static SolidColorBrush Purple
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_purple == null)
                {
                    _purple = new SolidColorBrush(Color.FromArgb(0xFF, 0x69, 0x22, 0x7B));
                    _purple.Freeze();
                }
                return _purple;
            }
        }

        private static SolidColorBrush _purple;

        [PublicAPI]
        public static SolidColorBrush Red
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_red == null)
                {
                    _red = new SolidColorBrush(Color.FromArgb(0xFF, 0xE5, 0x14, 0x00));
                    _red.Freeze();
                }
                return _red;
            }
        }

        private static SolidColorBrush _red;

        [PublicAPI]
        public static SolidColorBrush Rose
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_rose == null)
                {
                    _rose = new SolidColorBrush(Color.FromArgb(0xFF, 0xD8, 0x00, 0x73));
                    _rose.Freeze();
                }
                return _rose;
            }
        }

        private static SolidColorBrush _rose;

        [PublicAPI]
        public static SolidColorBrush Sky
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_sky == null)
                {
                    _sky = new SolidColorBrush(Color.FromArgb(0xFF, 0x1B, 0xA1, 0xE2));
                    _sky.Freeze();
                }
                return _sky;
            }
        }

        private static SolidColorBrush _sky;

        [PublicAPI]
        public static SolidColorBrush Viridian
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_viridian == null)
                {
                    _viridian = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xAB, 0xA9));
                    _viridian.Freeze();
                }
                return _viridian;
            }
        }

        private static SolidColorBrush _viridian;

        [PublicAPI]
        public static SolidColorBrush Violet
        {
            get
            {
                Contract.Ensures(Contract.Result<SolidColorBrush>() != null);
                if (_violet == null)
                {
                    _violet = new SolidColorBrush(Color.FromArgb(0xFF, 0xA2, 0x00, 0xFF));
                    _violet.Freeze();
                }
                return _violet;
            }
        }

        private static SolidColorBrush _violet;
    }
} ;