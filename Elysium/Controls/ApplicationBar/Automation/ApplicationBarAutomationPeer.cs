using System.Diagnostics.Contracts;
using System.Windows.Automation.Peers;

using JetBrains.Annotations;

namespace Elysium.Controls.Automation
{
    [PublicAPI]
    public class ApplicationBarAutomationPeer : FrameworkElementAutomationPeer
    {
        [PublicAPI]
        public ApplicationBarAutomationPeer([NotNull] ApplicationBar owner) : base(owner)
        {
        }

        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        protected override string GetClassNameCore()
        {
            Contract.Ensures(Contract.Result<string>() == "ApplicationBar");
            return "ApplicationBar";
        }

        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            Contract.Ensures(Contract.Result<AutomationControlType>() == AutomationControlType.Menu);
            return AutomationControlType.Menu;
        }
    }
} ;