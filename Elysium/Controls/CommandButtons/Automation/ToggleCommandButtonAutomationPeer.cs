using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

using JetBrains.Annotations;

namespace Elysium.Controls.Automation
{
    [PublicAPI]
    public class ToggleCommandButtonAutomationPeer : CommandButtonBaseAutomationPeer, IToggleProvider
    {
        [PublicAPI]
        public ToggleCommandButtonAutomationPeer([NotNull] ToggleCommandButton owner) : base(owner)
        {
        }

        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        protected override string GetClassNameCore()
        {
            Contract.Ensures(Contract.Result<string>() == "ToggleCommandButton");
            return "ToggleCommandButton";
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            return patternInterface == PatternInterface.Toggle ? this : base.GetPattern(patternInterface);
        }

        [PublicAPI]
        public void Toggle()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }

            var owner = (ToggleCommandButton)Owner;
            owner.OnToggle();
        }

        [PublicAPI]
        public ToggleState ToggleState
        {
            get
            {
                var owner = (ToggleCommandButton)Owner;
                return ConvertToToggleState(owner.IsChecked);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RaiseToggleStatePropertyChangedEvent(bool? oldValue, bool? newValue)
        {
            if (oldValue != newValue)
            {
                RaisePropertyChangedEvent(TogglePatternIdentifiers.ToggleStateProperty, ConvertToToggleState(oldValue), ConvertToToggleState(newValue));
            }
        }

        private static ToggleState ConvertToToggleState(bool? value)
        {
            switch (value)
            {
                case (null):
                    return ToggleState.Indeterminate;
                case (true):
                    return ToggleState.On;
                default:
                    return ToggleState.Off;
            }
        }
    }
} ;