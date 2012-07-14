using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

using JetBrains.Annotations;

namespace Elysium.Controls.Automation
{
    public class ToggleSwitchAutomationPeer : FrameworkElementAutomationPeer, IToggleProvider
    {
        public ToggleSwitchAutomationPeer([NotNull] ToggleSwitch owner) : base(owner)
        {
        }

        [PublicAPI]
        public new ToggleSwitch Owner
        {
            get
            {
                Contract.Ensures(Contract.Result<ToggleSwitch>() != null);
                var result = (ToggleSwitch)base.Owner;
                Contract.Assume(result != null);
                return result;
            }
        }

        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        protected override string GetClassNameCore()
        {
            Contract.Ensures(Contract.Result<string>() == "ToggleSwitch");
            return "ToggleSwitch";
        }

        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            Contract.Ensures(Contract.Result<AutomationControlType>() == AutomationControlType.Slider);
            return AutomationControlType.Slider;
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            return patternInterface == PatternInterface.Toggle ? this : base.GetPattern(patternInterface);
        }

        public void Toggle()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }

            Owner.OnToggle();
        }

        public ToggleState ToggleState
        {
            get { return ConvertToToggleState(Owner.IsChecked); }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RaiseToggleStatePropertyChangedEvent(bool oldValue, bool newValue)
        {
            if (oldValue != newValue)
            {
                RaisePropertyChangedEvent(TogglePatternIdentifiers.ToggleStateProperty, ConvertToToggleState(oldValue), ConvertToToggleState(newValue));
            }
        }

        private static ToggleState ConvertToToggleState(bool value)
        {
            switch (value)
            {
                case (true):
                    return ToggleState.On;
                default:
                    return ToggleState.Off;
            }
        }
    }
} ;