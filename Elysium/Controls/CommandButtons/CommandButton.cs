using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Automation.Peers;

using Elysium.Controls.Automation;
using Elysium.Controls.Primitives;

using JetBrains.Annotations;

namespace Elysium.Controls
{
    [PublicAPI]
    public class CommandButton : CommandButtonBase
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static CommandButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandButton), new FrameworkPropertyMetadata(typeof(CommandButton)));
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new CommandButtonAutomationPeer(this);
        }

        protected override void OnClick()
        {
            if (AutomationPeer.ListenerExists(AutomationEvents.InvokePatternOnInvoked))
            {
                var peer = UIElementAutomationPeer.CreatePeerForElement(this);
                if (peer != null)
                {
                    peer.RaiseAutomationEvent(AutomationEvents.InvokePatternOnInvoked);
                }
            }
            base.OnClick();
        }

        internal void OnClickInternal()
        {
            OnClick();
        }
    }
} ;