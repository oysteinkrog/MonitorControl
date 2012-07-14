using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

using JetBrains.Annotations;

namespace Elysium.Controls.Automation
{
    [PublicAPI]
    public class DropDownCommandButtonAutomationPeer : CommandButtonBaseAutomationPeer, IExpandCollapseProvider
    {
        [PublicAPI]
        public DropDownCommandButtonAutomationPeer([NotNull] DropDownCommandButton owner) : base(owner)
        {
        }

        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        protected override string GetClassNameCore()
        {
            Contract.Ensures(Contract.Result<string>() == "DropDownCommandButton");
            return "DropDownCommandButton";
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            return patternInterface == PatternInterface.ExpandCollapse ? this : base.GetPattern(patternInterface);
        }

        [PublicAPI]
        public void Expand()
        {
            IsEnabledAndHasSubmenu();
            var owner = (DropDownCommandButton)Owner;
            owner.IsDropDownOpen = true;
        }

        [PublicAPI]
        public void Collapse()
        {
            IsEnabledAndHasSubmenu();
            var owner = (DropDownCommandButton)Owner;
            owner.IsDropDownOpen = false;
        }

        [DebuggerHidden]
        [ContractArgumentValidator]
        private void IsEnabledAndHasSubmenu()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            if (!((DropDownCommandButton)Owner).HasSubmenu)
            {
                throw new InvalidOperationException("Operation can't be perform");
            }
            Contract.EndContractBlock();
        }

        [PublicAPI]
        public ExpandCollapseState ExpandCollapseState
        {
            get
            {
                var owner = (DropDownCommandButton)Owner;
                return !owner.HasSubmenu ? ExpandCollapseState.LeafNode : (owner.IsDropDownOpen ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void RaiseExpandCollapseStatePropertyChangedEvent(ExpandCollapseState oldValue, ExpandCollapseState newValue)
        {
            if (oldValue != newValue)
            {
                RaisePropertyChangedEvent(ExpandCollapsePatternIdentifiers.ExpandCollapseStateProperty, oldValue, newValue);
            }
        }
    }
} ;