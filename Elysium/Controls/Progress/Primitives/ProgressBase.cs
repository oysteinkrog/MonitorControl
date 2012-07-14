using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

using Elysium.Extensions;

using JetBrains.Annotations;

using ProgressAutomationPeer = Elysium.Controls.Automation.ProgressAutomationPeer;

namespace Elysium.Controls.Primitives
{
    [PublicAPI]
    [TemplatePart(Name = TrackName, Type = typeof(FrameworkElement))]
    public abstract class ProgressBase : RangeBase
    {
        private const string TrackName = "PART_Track";

        internal FrameworkElement Track;

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ProgressBase()
        {
            MaximumProperty.OverrideMetadata(typeof(ProgressBase), new FrameworkPropertyMetadata(100d));
        }

        private static readonly DependencyPropertyKey PercentPropertyKey =
            DependencyProperty.RegisterReadOnly("Percent", typeof(double), typeof(ProgressBase),
                                                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.None, OnPercentChanged));

        [PublicAPI]
        public static readonly DependencyProperty PercentProperty = PercentPropertyKey.DependencyProperty;

        [PublicAPI]
        [Browsable(false)]
        public double Percent
        {
            get { return BoxingHelper<double>.Unbox(GetValue(PercentProperty)); }
            private set { SetValue(PercentPropertyKey, value); }
        }

        private static void OnPercentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var instance = (ProgressBase)obj;
            instance.OnPercentChanged(BoxingHelper<double>.Unbox(e.OldValue), BoxingHelper<double>.Unbox(e.NewValue));
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnPercentChanged(double oldPercent, double newPercent)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            Percent = State != ProgressState.Normal || Maximum <= Minimum ? double.NaN : (Value - Minimum) / (Maximum - Minimum);
        }

        [PublicAPI]
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(ProgressState), typeof(ProgressBase),
                                        new FrameworkPropertyMetadata(ProgressState.Normal, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                      OnStateChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines the state of control.")]
        public ProgressState State
        {
            get { return BoxingHelper<ProgressState>.Unbox(GetValue(StateProperty)); }
            set { SetValue(StateProperty, value); }
        }

        private static void OnStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var progressBar = (ProgressBase)obj;
            progressBar.OnStateChanged(BoxingHelper<ProgressState>.Unbox(e.OldValue), BoxingHelper<ProgressState>.Unbox(e.NewValue));
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnStateChanged(ProgressState oldState, ProgressState newState)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
            var peer = UIElementAutomationPeer.FromElement(this) as ProgressAutomationPeer;
            if (peer != null)
            {
                peer.InvalidatePeer();
            }

            if (IsEnabled)
            {
                switch (newState)
                {
                    case ProgressState.Busy:
                        VisualStateManager.GoToState(this, "Busy", true);
                        if (IndeterminateAnimation != null && IndeterminateAnimation.GetCurrentState() != ClockState.Stopped)
                        {
                            IsIndeterminateAnimationRunning = false;
                            IndeterminateAnimation.Stop(this);
                        }
                        if (BusyAnimation != null)
                        {
                            BusyAnimation.Begin(this, Template, true);
                            IsBusyAnimationRunning = true;
                        }
                        break;
                    case ProgressState.Indeterminate:
                        VisualStateManager.GoToState(this, "Indeterminate", true);
                        if (BusyAnimation != null && BusyAnimation.GetCurrentState() != ClockState.Stopped)
                        {
                            IsBusyAnimationRunning = false;
                            BusyAnimation.Stop(this);
                        }
                        if (IndeterminateAnimation != null)
                        {
                            IndeterminateAnimation.Begin(this, Template, true);
                            IsIndeterminateAnimationRunning = true;
                        }
                        break;
                    case ProgressState.Normal:
                        VisualStateManager.GoToState(this, "Normal", true);
                        if (IndeterminateAnimation != null && IndeterminateAnimation.GetCurrentState() != ClockState.Stopped)
                        {
                            IsIndeterminateAnimationRunning = false;
                            IndeterminateAnimation.Stop(this);
                        }
                        if (BusyAnimation != null && BusyAnimation.GetCurrentState() != ClockState.Stopped)
                        {
                            IsBusyAnimationRunning = false;
                            BusyAnimation.Stop(this);
                        }
                        break;
                }
            }
        }

        internal const string DefaultIndeterminateAnimationName = "CF98B9E7AB2F4CBD9EA654552441CD6A";

        [PublicAPI]
        public static readonly DependencyProperty IndeterminateAnimationProperty =
            DependencyProperty.Register("IndeterminateAnimation", typeof(Storyboard), typeof(ProgressBase),
                                        new FrameworkPropertyMetadata(
                                            null, //new Storyboard { Name = DefaultIndeterminateAnimationName, RepeatBehavior = RepeatBehavior.Forever },
                                            FrameworkPropertyMetadataOptions.AffectsRender));

        [PublicAPI]
        [Category("Appearance")]
        [Description("Determines the animation that playing in Indeterminate state.")]
        public Storyboard IndeterminateAnimation
        {
            get { return (Storyboard)GetValue(IndeterminateAnimationProperty); }
            set { SetValue(IndeterminateAnimationProperty, value); }
        }

        [PublicAPI]
        protected static readonly DependencyPropertyKey IsIndeterminateAnimationRunningPropertyKey =
            DependencyProperty.RegisterReadOnly("IsIndeterminateAnimationRunning", typeof(bool), typeof(ProgressBase),
                                                new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        public static readonly DependencyProperty IsIndeterminateAnimationRunningProperty = IsIndeterminateAnimationRunningPropertyKey.DependencyProperty;

        [PublicAPI]
        [Browsable(false)]
        public bool IsIndeterminateAnimationRunning
        {
            get { return (bool)GetValue(IsIndeterminateAnimationRunningProperty); }
            protected set { SetValue(IsIndeterminateAnimationRunningPropertyKey, value); }
        }

        internal const string DefaultBusyAnimationName = "B45C62BF28AC49FDB8F172249BF56E5B";

        [PublicAPI]
        public static readonly DependencyProperty BusyAnimationProperty =
            DependencyProperty.Register("BusyAnimation", typeof(Storyboard), typeof(ProgressBase),
                                        new FrameworkPropertyMetadata(
                                            null, //new Storyboard { Name = DefaultBusyAnimationName, RepeatBehavior = RepeatBehavior.Forever },
                                            FrameworkPropertyMetadataOptions.AffectsRender));

        [PublicAPI]
        [Category("Appearance")]
        [Description("Determines the animation that playing in Busy state.")]
        public Storyboard BusyAnimation
        {
            get { return (Storyboard)GetValue(BusyAnimationProperty); }
            set { SetValue(BusyAnimationProperty, value); }
        }

        [PublicAPI]
        protected static readonly DependencyPropertyKey IsBusyAnimationRunningPropertyKey =
            DependencyProperty.RegisterReadOnly("IsBusyAnimationRunning", typeof(bool), typeof(ProgressBase),
                                                new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, FrameworkPropertyMetadataOptions.None));

        [PublicAPI]
        public static readonly DependencyProperty IsBusyAnimationRunningProperty = IsBusyAnimationRunningPropertyKey.DependencyProperty;

        [PublicAPI]
        [Browsable(false)]
        public bool IsBusyAnimationRunning
        {
            get { return (bool)GetValue(IsBusyAnimationRunningProperty); }
            protected set { SetValue(IsBusyAnimationRunningPropertyKey, value); }
        }

        [PublicAPI]
        public static readonly RoutedEvent AnimationsUpdatingEvent = EventManager.RegisterRoutedEvent("AnimationsUpdating", RoutingStrategy.Tunnel,
                                                                                                      typeof(RoutedEventHandler), typeof(ProgressBase));

        [PublicAPI]
        [Category("Appearance")]
        [Description("Occurs when a state's animations updating.")]
        public event RoutedEventHandler AnimationsUpdating
        {
            add { AddHandler(AnimationsUpdatingEvent, value); }
            remove { RemoveHandler(AnimationsUpdatingEvent, value); }
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnAnimationsUpdating(RoutedEventArgs e)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly RoutedEvent AnimationsUpdatedEvent = EventManager.RegisterRoutedEvent("AnimationsUpdated", RoutingStrategy.Bubble,
                                                                                                     typeof(RoutedEventHandler), typeof(ProgressBase));

        [PublicAPI]
        [Category("Appearance")]
        [Description("Occurs when a state's animations updated.")]
        public event RoutedEventHandler AnimationsUpdated
        {
            add { AddHandler(AnimationsUpdatedEvent, value); }
            remove { RemoveHandler(AnimationsUpdatedEvent, value); }
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnAnimationsUpdated(RoutedEventArgs e)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
            RaiseEvent(e);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            OnApplyTemplateInternal();
        }

        [SecuritySafeCritical]
        internal virtual void OnApplyTemplateInternal()
        {
            if (Template != null)
            {
                Track = Template.FindName(TrackName, this) as FrameworkElement;

                if (Track == null)
                {
                    Trace.TraceWarning(TrackName + " not found.");
                }
                else
                {
                    Track.SizeChanged += (sender, e) =>
                    {
                        OnAnimationsUpdating(new RoutedEventArgs(AnimationsUpdatingEvent));
                        OnAnimationsUpdated(new RoutedEventArgs(AnimationsUpdatedEvent));
                    };
                }
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ProgressAutomationPeer(this);
        }
    }
} ;