using System.Diagnostics.Contracts;
using System.Windows.Automation.Peers;

using Elysium.Controls.Primitives;

using JetBrains.Annotations;

namespace Elysium.Controls.Automation
{
    [PublicAPI]
    public abstract class CommandButtonBaseAutomationPeer : ButtonBaseAutomationPeer
    {
        [PublicAPI]
        protected CommandButtonBaseAutomationPeer([NotNull] CommandButtonBase owner) : base(owner)
        {
        }

        [PublicAPI]
        public new CommandButtonBase Owner
        {
            get
            {
                Contract.Ensures(Contract.Result<CommandButtonBase>() != null);
                var result = (CommandButtonBase)base.Owner;
                Contract.Assume(result != null);
                return result;
            }
        }

        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            Contract.Ensures(Contract.Result<AutomationControlType>() == AutomationControlType.Button);
            return AutomationControlType.Button;
        }
    }
} ;