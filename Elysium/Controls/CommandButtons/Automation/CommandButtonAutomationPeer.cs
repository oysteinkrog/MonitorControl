using System.Diagnostics.Contracts;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

using JetBrains.Annotations;

namespace Elysium.Controls.Automation
{
    [PublicAPI]
    public class CommandButtonAutomationPeer : CommandButtonBaseAutomationPeer, IInvokeProvider
    {
        [PublicAPI]
        public CommandButtonAutomationPeer([NotNull] CommandButton owner) : base(owner)
        {
        }

        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        protected override string GetClassNameCore()
        {
            Contract.Ensures(Contract.Result<string>() == "CommandButton");
            return "CommandButton";
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            return patternInterface == PatternInterface.Invoke ? this : base.GetPattern(patternInterface);
        }

        [PublicAPI]
        public void Invoke()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }

            Dispatcher.BeginInvoke(DispatcherPriority.Input, new DispatcherOperationCallback(
                                                                 delegate
                                                                 {
                                                                     ((CommandButton)Owner).OnClickInternal();
                                                                     return null;
                                                                 }), null);
        }
    }
} ;