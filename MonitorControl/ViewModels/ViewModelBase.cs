using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace MonitorControl.ViewModels
{
    [PublicAPI]
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        [PublicAPI]
        public event PropertyChangedEventHandler PropertyChanged;

        [PublicAPI]
        protected bool ThrowOnInvalidPropertyName
        {
            get { return _throwOnInvalidPropertyName; }
            set { _throwOnInvalidPropertyName = value; }
        }

        private bool _throwOnInvalidPropertyName = true;

        [PublicAPI]
        public bool Disposed { get; set; }

        [PublicAPI]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnPropertyChanged(string propertyName)
        // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            VerifyPropertyName(propertyName);
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [PublicAPI]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }
            Contract.EndContractBlock();
            OnPropertyChanged(((MemberExpression)propertyExpression.Body).Member.Name);
        }

        [PublicAPI]
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                var message = "Invalid property name: " + propertyName;

                if (ThrowOnInvalidPropertyName)
                {
                    throw new MemberAccessException(message);
                }
                Debug.Fail(message);
            }
        }

        [PublicAPI]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ViewModelBase()
        {
            Dispose(false);
        }

        [PublicAPI]
        // ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void Dispose(bool disposing)
        // ReSharper restore VirtualMemberNeverOverriden.Global
        {
            Contract.Ensures(Disposed);
            Disposed = true;
        }
    }
} ;