using System;
using MonitorControl.ViewModels;

namespace MonitorControl.Model
{

    public static class MinMaxValExtensions
    {
        public static double MinMaxValToPercent(this MinMaxVal<uint> v)
        {
            return v.Val / (double)(v.Max - v.Min);
        }

        public static uint ValFromPercent(this MinMaxVal<uint> v, double p)
        {
            return (uint)Math.Round(((v.Max - v.Min) * p));
        }
    }

    public class MinMaxValUint : MinMaxVal<uint>
    {
        public MinMaxValUint(Func<Tuple<uint, uint, uint>> func, Action<uint, uint, uint> set)
        {
            Update = func;
            Set = set;
        }
    }

    public class MinMaxVal<T> : ViewModelBase
    {
        public Action<T, T, T> Set { get; protected set; }
        public Func<Tuple<T, T, T>> Update { get; protected set; }

        private T _max;
        private T _min;
        private T _val;
        
        public MinMaxVal()
        {
        }

        public MinMaxVal(Func<Tuple<T, T, T>> func, Action<T, T, T> set)
        {
            Update = func;
            Set = set;
        }

        public T Min
        {
            get
            {
                Tuple<T, T, T> u = Update();
                _min = u.Item1;
                _val = u.Item2;
                _max = u.Item3;
                return _min;
            }
            set
            {
                _min = value;
                Set(_min, value, _max);
            }
        }

        public T Max
        {
            get
            {
                Tuple<T, T, T> u = Update();
                _min = u.Item1;
                _val = u.Item2;
                _max = u.Item3;
                return _max;
            }
            set
            {
                _max = value;
                Set(_min, value, _max);
            }
        }

        public T Val
        {
            get
            {
                Tuple<T, T, T> u = Update();
                _min = u.Item1;
                _val = u.Item2;
                _max = u.Item3;
                return _val;
            }
            set
            {
                _val = value;
                Set(_min, value, _max);
            }
        }
    }
}