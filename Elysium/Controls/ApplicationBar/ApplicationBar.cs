using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

using Elysium.Controls.Automation;
using Elysium.Controls.Primitives;
using Elysium.Extensions;
using Elysium.Parameters;

using JetBrains.Annotations;

namespace Elysium.Controls
{
    [PublicAPI]
    [DefaultEvent("Opened")]
    [Localizability(LocalizationCategory.Menu)]
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(CommandButton))]
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
    public class ApplicationBar : ItemsControl
// ReSharper restore ClassWithVirtualMembersNeverInherited.Global
    {
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ApplicationBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(typeof(ApplicationBar)));

            var itemsPanelTemplate = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(ApplicationBarPanel)));
            itemsPanelTemplate.Seal();
            ItemsPanelProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(itemsPanelTemplate));

            IsTabStopProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(false));
            FocusableProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(false));
            FocusManager.IsFocusScopeProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(true));
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(
                typeof(ApplicationBar), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(
                typeof(ApplicationBar), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
            KeyboardNavigation.ControlTabNavigationProperty.OverrideMetadata(
                typeof(ApplicationBar), new FrameworkPropertyMetadata(KeyboardNavigationMode.Once));

            HorizontalAlignmentProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(HorizontalAlignment.Stretch));
            VerticalAlignmentProperty.OverrideMetadata(typeof(ApplicationBar), new FrameworkPropertyMetadata(VerticalAlignment.Bottom));

            EventManager.RegisterClassHandler(typeof(ApplicationBar), Mouse.LostMouseCaptureEvent,
                                              new MouseEventHandler(OnLostMouseCapture));
            EventManager.RegisterClassHandler(typeof(ApplicationBar), Mouse.PreviewMouseUpOutsideCapturedElementEvent,
                                              new MouseButtonEventHandler(OnPreviewMouseButtonOutsideCapturedElement));
        }

        [PublicAPI]
        public static readonly DependencyProperty DockProperty =
            DependencyProperty.RegisterAttached("Dock", typeof(ApplicationBarDock), typeof(ApplicationBar),
                                                new FrameworkPropertyMetadata(ApplicationBarDock.Left, FrameworkPropertyMetadataOptions.AffectsArrange));

        [PublicAPI]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        [AttachedPropertyBrowsableForChildren]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static ApplicationBarDock GetDock([NotNull] UIElement obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return BoxingHelper<ApplicationBarDock>.Unbox(obj.GetValue(DockProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetDock([NotNull] UIElement obj, ApplicationBarDock value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(DockProperty, value);
        }

        [PublicAPI]
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ApplicationBar),
                                        new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                      OnIsOpenChanged));

        [PublicAPI]
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Indicates whether the ApplicationBar is visible.")]
        public bool IsOpen
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(IsOpenProperty)); }
            set { SetValue(IsOpenProperty, BooleanBoxingHelper.Box(value)); }
        }

        private bool _isOpen;

        private static void OnIsOpenChanged([NotNull] DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var instance = (ApplicationBar)obj;
            instance.OnIsOpenChanged(BooleanBoxingHelper.Unbox(e.OldValue), BooleanBoxingHelper.Unbox(e.NewValue));
        }

        internal bool IsOpening;
        internal bool IsClosing;

        [PublicAPI]
        protected virtual void OnIsOpenChanged(bool oldIsOpen, bool newIsOpen)
        {
            if (newIsOpen && !oldIsOpen)
            {
                IsOpening = true;

                OnOpening(new RoutedEventArgs(OpeningEvent, this));

                if (TransitionMode == ApplicationBarTransitionMode.None)
                {
                    Mouse.Capture(this, CaptureMode.SubTree);

                    _isOpen = true;
                    InvalidateArrange();

                    OnOpened(new RoutedEventArgs(OpenedEvent, this));

                    IsOpening = false;
                }
                else
                {
                    var storyboard = new Storyboard
                                         {
                                             FillBehavior = FillBehavior.Stop
                                         };

                    Timeline animation;
                    switch (TransitionMode)
                    {
                        case ApplicationBarTransitionMode.Fade:
                            animation = new DoubleAnimation(0d, 1d, General.GetMinimumDuration(this));
                            Storyboard.SetTarget(animation, this);
                            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
                            // NOTE: Lack of contracts
                            Contract.Assume(storyboard.Children != null);
                            storyboard.Children.Add(animation);
                            break;
                        case ApplicationBarTransitionMode.Slide:
                            animation = new DoubleAnimation(0d, DesiredSize.Height, General.GetMinimumDuration(this));
                            Storyboard.SetTarget(animation, this);
                            Storyboard.SetTargetProperty(animation, new PropertyPath("Height"));
                            // NOTE: Lack of contracts
                            Contract.Assume(storyboard.Children != null);
                            storyboard.Children.Add(animation);
                            break;
                    }

                    storyboard.Completed += (sender, e) =>
                    {
                        storyboard.Stop(this);
                        storyboard.Remove(this);

                        OnOpened(new RoutedEventArgs(OpenedEvent, this));

                        IsOpening = false;
                    };

                    if (!StaysOpen)
                    {
                        Mouse.Capture(this, CaptureMode.SubTree);
                    }

                    storyboard.Freeze();
                    storyboard.Begin(this, true);

                    _isOpen = true;
                    InvalidateArrange();
                }
            }
            if (!newIsOpen && oldIsOpen)
            {
                IsClosing = true;

                OnClosing(new RoutedEventArgs(ClosingEvent, this));

                if (TransitionMode == ApplicationBarTransitionMode.None)
                {
                    Mouse.Capture(null);

                    _isOpen = false;
                    InvalidateArrange();

                    OnClosed(new RoutedEventArgs(ClosedEvent, this));

                    IsClosing = false;
                }
                else
                {
                    var storyboard = new Storyboard
                                         {
                                             FillBehavior = FillBehavior.Stop
                                         };

                    Timeline animation;
                    switch (TransitionMode)
                    {
                        case ApplicationBarTransitionMode.Fade:
                            animation = new DoubleAnimation(1d, 0d, General.GetMinimumDuration(this));
                            Storyboard.SetTarget(animation, this);
                            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
                            // NOTE: Lack of contracts
                            Contract.Assume(storyboard.Children != null);
                            storyboard.Children.Add(animation);
                            break;
                        case ApplicationBarTransitionMode.Slide:
                            animation = new DoubleAnimation(DesiredSize.Height, 0d, General.GetMinimumDuration(this));
                            Storyboard.SetTarget(animation, this);
                            Storyboard.SetTargetProperty(animation, new PropertyPath("Height"));
                            // NOTE: Lack of contracts
                            Contract.Assume(storyboard.Children != null);
                            storyboard.Children.Add(animation);
                            break;
                    }

                    storyboard.Completed += (sender, e) =>
                    {
                        storyboard.Stop(this);
                        storyboard.Remove(this);

                        if (!StaysOpen)
                        {
                            Mouse.Capture(null);
                        }

                        _isOpen = false;
                        InvalidateArrange();

                        OnClosed(new RoutedEventArgs(ClosedEvent, this));

                        IsClosing = false;
                    };

                    storyboard.Freeze();
                    storyboard.Begin(this, true);
                }
            }
        }

        [PublicAPI]
        public static readonly RoutedEvent OpeningEvent =
            EventManager.RegisterRoutedEvent("Opening", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(ApplicationBar));

        [PublicAPI]
        [Category("Behavior")]
        [Description("Occurs before opening ApplicationBar instance.")]
        public event RoutedEventHandler Opening
        {
            add { AddHandler(OpeningEvent, value); }
            remove { RemoveHandler(OpeningEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnOpening(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly RoutedEvent OpenedEvent =
            EventManager.RegisterRoutedEvent("Opened", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ApplicationBar));

        [PublicAPI]
        [Category("Behavior")]
        [Description("Occurs after ApplicationBar instance is opened.")]
        public event RoutedEventHandler Opened
        {
            add { AddHandler(OpenedEvent, value); }
            remove { RemoveHandler(OpenedEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnOpened(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly RoutedEvent ClosingEvent =
            EventManager.RegisterRoutedEvent("Closing", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(ApplicationBar));

        [PublicAPI]
        [Category("Behavior")]
        [Description("Occurs before closing ApplicationBar instance.")]
        public event RoutedEventHandler Closing
        {
            add { AddHandler(ClosingEvent, value); }
            remove { RemoveHandler(ClosingEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnClosing(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly RoutedEvent ClosedEvent =
            EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ApplicationBar));

        [PublicAPI]
        [Category("Behavior")]
        [Description("Occurs after ApplicationBar instance is closed.")]
        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        [PublicAPI]
        protected virtual void OnClosed(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        [PublicAPI]
        public static readonly DependencyProperty StaysOpenProperty =
            DependencyProperty.Register("StaysOpen", typeof(bool), typeof(ApplicationBar),
                                        new FrameworkPropertyMetadata(BooleanBoxingHelper.TrueBox, FrameworkPropertyMetadataOptions.None, OnStaysOpenChanged));

        [PublicAPI]
        [Category("Behavior")]
        [Description("Indicates whether the ApplicationBar control closes when the control is no longer in focus.")]
        public bool StaysOpen
        {
            get { return BooleanBoxingHelper.Unbox(GetValue(StaysOpenProperty)); }
            set { SetValue(StaysOpenProperty, BooleanBoxingHelper.Box(value)); }
        }

        private static void OnStaysOpenChanged([NotNull] DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var instance = (ApplicationBar)obj;
            instance.OnStaysOpenChanged(BooleanBoxingHelper.Unbox(e.OldValue), BooleanBoxingHelper.Unbox(e.NewValue));
        }

        [PublicAPI]
        protected virtual void OnStaysOpenChanged(bool oldStaysOpen, bool newStaysOpen)
        {
        }

        [PublicAPI]
        public static readonly DependencyProperty PreventsOpenProperty =
            DependencyProperty.RegisterAttached("PreventsOpen", typeof(bool), typeof(ApplicationBar),
                                                new FrameworkPropertyMetadata(BooleanBoxingHelper.FalseBox, FrameworkPropertyMetadataOptions.Inherits));

        [PublicAPI]
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static bool GetPreventsOpen([NotNull] UIElement obj)
        {
            ValidationHelper.NotNull(obj, () => obj);
            return BooleanBoxingHelper.Unbox(obj.GetValue(PreventsOpenProperty));
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetPreventsOpen([NotNull] UIElement obj, bool value)
        {
            ValidationHelper.NotNull(obj, () => obj);
            obj.SetValue(PreventsOpenProperty, BooleanBoxingHelper.Box(value));
        }

        [PublicAPI]
        public static readonly DependencyProperty TransitionModeProperty =
            DependencyProperty.Register("TransitionMode", typeof(ApplicationBarTransitionMode), typeof(ApplicationBar),
                                        new FrameworkPropertyMetadata(ApplicationBarTransitionMode.Slide, FrameworkPropertyMetadataOptions.None,
                                                                      OnTransitionModeChanged));

        [PublicAPI]
        [Category("Appearance")]
        [Description("Animation for the opening and closing of a ApplicationBar.")]
        public ApplicationBarTransitionMode TransitionMode
        {
            get { return BoxingHelper<ApplicationBarTransitionMode>.Unbox(GetValue(TransitionModeProperty)); }
            set { SetValue(TransitionModeProperty, value); }
        }

        private static void OnTransitionModeChanged([NotNull] DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var instance = (ApplicationBar)obj;
            instance.OnTransitionModeChanged(BoxingHelper<ApplicationBarTransitionMode>.Unbox(e.OldValue),
                                             BoxingHelper<ApplicationBarTransitionMode>.Unbox(e.NewValue));
        }

        [PublicAPI]
        protected virtual void OnTransitionModeChanged(ApplicationBarTransitionMode oldTransitionMode, ApplicationBarTransitionMode newTransitionMode)
        {
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is CommandButtonBase;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CommandButton();
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ApplicationBarAutomationPeer(this);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            return _isOpen ? base.ArrangeOverride(arrangeBounds) : new Size(0, 0);
        }

        private static void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            var instance = sender as ApplicationBar;
            var source = e.OriginalSource as DependencyObject;
            if (instance != null && source != null && !instance.StaysOpen)
            {
                var visualParent = VisualTreeHelperExtensions.FindParent<ApplicationBar>(source);
                var logicalParent = LogicalTreeHelperExtensions.FindParent<ApplicationBar>(source);
                if (Equals(instance, visualParent) || Equals(instance, logicalParent) || (Mouse.Captured == null && instance.IsOpen))
                {
                    Mouse.Capture(instance, CaptureMode.SubTree);
                    e.Handled = true;
                }
            }
        }

        private static void OnPreviewMouseButtonOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            var instance = sender as ApplicationBar;
            var source = e.OriginalSource as DependencyObject;
            if (instance != null && source != null && !instance.StaysOpen)
            {
                var visualParent = VisualTreeHelperExtensions.FindParent<ApplicationBar>(source);
                var logicalParent = LogicalTreeHelperExtensions.FindParent<ApplicationBar>(source);
                if (!(Equals(instance, visualParent) || Equals(instance, logicalParent)))
                {
                    instance.IsOpen = false;
                }
            }
        }
    }
} ;