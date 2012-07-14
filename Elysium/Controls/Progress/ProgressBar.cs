using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Elysium.Controls.Primitives;
using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Controls
{
    [PublicAPI]
    [TemplatePart(Name = IndicatorName, Type = typeof(Rectangle))]
    [TemplatePart(Name = BusyBarName, Type = typeof(Canvas))]
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
    public class ProgressBar : ProgressBase
// ReSharper restore ClassWithVirtualMembersNeverInherited.Global
    {
        private const string IndicatorName = "PART_Indicator";
        private const string BusyBarName = "PART_BusyBar";

        private Rectangle _indicator;
        private Canvas _busyBar;

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(typeof(ProgressBar)));
        }

        [PublicAPI]
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ProgressBar),
                                        new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure, OnOrientationChanged),
                                        IsValidOrientation);

        [PublicAPI]
        [Category("Appearance")]
        [Description("Determines orientation of control.")]
        public Orientation Orientation
        {
            get { return BoxingHelper<Orientation>.Unbox(GetValue(OrientationProperty)); }
            set { SetValue(OrientationProperty, value); }
        }

        private static void OnOrientationChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ValidationHelper.NotNull(obj, () => obj);
            var instance = (ProgressBar)obj;
            instance.OnOrientationChanged(BoxingHelper<Orientation>.Unbox(e.OldValue), BoxingHelper<Orientation>.Unbox(e.NewValue));
        }

        [PublicAPI]
// ReSharper disable VirtualMemberNeverOverriden.Global
        protected virtual void OnOrientationChanged(Orientation oldOrientation, Orientation newOrientation)
// ReSharper restore VirtualMemberNeverOverriden.Global
        {
        }

        private static bool IsValidOrientation(object value)
        {
            var orientation = BoxingHelper<Orientation>.Unbox(value);
            return orientation == Orientation.Horizontal || orientation == Orientation.Vertical;
        }

        [SecuritySafeCritical]
        internal override void OnApplyTemplateInternal()
        {
            if (Template != null)
            {
                _indicator = Template.FindName(IndicatorName, this) as Rectangle;
                if (_indicator == null)
                {
                    Trace.TraceWarning(IndicatorName + " not found.");
                }
                // NOTE: Lack of contracts: FindName is pure method
                Contract.Assume(Template != null);
                _busyBar = Template.FindName(BusyBarName, this) as Canvas;
                if (_busyBar == null)
                {
                    Trace.TraceWarning(BusyBarName + " not found.");
                }
            }

            base.OnApplyTemplateInternal();
        }

        protected override void OnAnimationsUpdating(RoutedEventArgs e)
        {
            base.OnAnimationsUpdating(e);

            UpdateIndeterminateAnimation();

            UpdateBusyAnimation();
        }

        private void UpdateIndeterminateAnimation()
        {
            if ((IndeterminateAnimation == null || (IndeterminateAnimation != null && IndeterminateAnimation.Name == DefaultIndeterminateAnimationName)) && Track != null && _indicator != null)
            {
                if (IndeterminateAnimation != null && IsIndeterminateAnimationRunning)
                {
                    IsIndeterminateAnimationRunning = false;
                    IndeterminateAnimation.Stop(this);
                    IndeterminateAnimation.Remove(this);
                }

                IndeterminateAnimation = new Storyboard { Name = DefaultIndeterminateAnimationName, RepeatBehavior = RepeatBehavior.Forever };

                var indicatorSize = Orientation == Orientation.Horizontal ? _indicator.Width : _indicator.Height;

                var trackSize = Orientation == Orientation.Horizontal ? Track.ActualWidth : Track.ActualHeight;

                var time = trackSize / 100;

                var animation = new DoubleAnimationUsingKeyFrames { Duration = new Duration(TimeSpan.FromSeconds(time + 0.5)) };
                // NOTE: Lack of contracts: DoubleAnimationUsingKeyFrames.KeyFrames is always have collection instance
                Contract.Assume(animation.KeyFrames != null);
                animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(-indicatorSize - 1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
                animation.KeyFrames.Add(new LinearDoubleKeyFrame(trackSize + 1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time))));

                Storyboard.SetTarget(animation, _indicator);
                Storyboard.SetTargetProperty(animation,
                                             new PropertyPath(Orientation == Orientation.Horizontal ? Canvas.LeftProperty : Canvas.TopProperty));

                // NOTE: Lack of contracts
                Contract.Assume(IndeterminateAnimation != null);
                Contract.Assume(IndeterminateAnimation.Children != null);
                IndeterminateAnimation.Children.Add(animation);

                if (IndeterminateAnimation.CanFreeze)
                {
                    IndeterminateAnimation.Freeze();
                }

                if (State == ProgressState.Indeterminate && IsEnabled)
                {
                    IndeterminateAnimation.Begin(this, Template, true);
                    IsIndeterminateAnimationRunning = true;
                }
            }
        }

        private void UpdateBusyAnimation()
        {
            if ((BusyAnimation == null || (BusyAnimation != null && BusyAnimation.Name == DefaultBusyAnimationName)) && Track != null && _busyBar != null)
            {
                if (BusyAnimation != null && IsBusyAnimationRunning)
                {
                    IsBusyAnimationRunning = false;
                    BusyAnimation.Stop(this);
                    BusyAnimation.Remove(this);
                }

                // NOTE: Lack of contracts: Children always have collection instance
                Contract.Assume(_busyBar.Children != null);

                BusyAnimation = new Storyboard { Name = DefaultBusyAnimationName, RepeatBehavior = RepeatBehavior.Forever };

                const double time = 0.25;
                const double durationTime = time * 2;
                const double beginTimeIncrement = time / 2;
                const double shortPauseTime = time;
                const double longPauseTime = time * 1.5;
                var partMotionTime = (_busyBar.Children.Count - 1) * beginTimeIncrement + durationTime;

                var busyAnimations = new Collection<DoubleAnimation>();

                var width = Track.ActualWidth;
                var height = Track.ActualHeight;

                for (var i = 0; i < _busyBar.Children.Count; i++)
                {
                    var element = (FrameworkElement)_busyBar.Children[_busyBar.Children.Count - i - 1];

                    if (element != null)
                    {
                        var elementWidth = element.Width;
                        var elementHeight = element.Height;

                        var index = (_busyBar.Children.Count - 1) / 2 - i;

                        var center = (Orientation == Orientation.Horizontal ? width : height) / 2;
                        var margin = Orientation == Orientation.Horizontal ? elementWidth : elementHeight;

                        var startPosition = -(Orientation == Orientation.Horizontal ? elementWidth : elementHeight) - 1;
                        var endPosition = center + index * ((Orientation == Orientation.Horizontal ? elementWidth : elementHeight) + margin);

                        var duration = new Duration(TimeSpan.FromSeconds(durationTime));
                        var animation = new DoubleAnimation(startPosition, endPosition, duration) { BeginTime = TimeSpan.FromSeconds(i * beginTimeIncrement) };
                        Storyboard.SetTarget(animation, element);
                        Storyboard.SetTargetProperty(animation,
                                                     new PropertyPath(Orientation == Orientation.Horizontal ? Canvas.LeftProperty : Canvas.TopProperty));

                        busyAnimations.Add(animation);
                    }
                }

                for (var i = 0; i < _busyBar.Children.Count; i++)
                {
                    var element = (FrameworkElement)_busyBar.Children[_busyBar.Children.Count - i - 1];


                    if (element != null)
                    {
                        var elementWidth = element.Width;
                        var elementHeight = element.Height;

                        var endPosition = (Orientation == Orientation.Horizontal ? width : height) +
                                          (Orientation == Orientation.Horizontal ? elementWidth : elementHeight) + 1;

                        var duration = new Duration(TimeSpan.FromSeconds(durationTime));
                        var animation = new DoubleAnimation(endPosition, duration) { BeginTime = TimeSpan.FromSeconds(partMotionTime + shortPauseTime + i * beginTimeIncrement) };
                        Storyboard.SetTarget(animation, element);
                        Storyboard.SetTargetProperty(animation,
                                                     new PropertyPath(Orientation == Orientation.Horizontal ? Canvas.LeftProperty : Canvas.TopProperty));

                        busyAnimations.Add(animation);
                    }
                }

                BusyAnimation.Duration = new Duration(TimeSpan.FromSeconds(partMotionTime * 2 + shortPauseTime + longPauseTime));

                // NOTE: Lack of contracts: Children always have collection instance
                Contract.Assume(BusyAnimation.Children != null);
                foreach (var animation in busyAnimations)
                {
                    BusyAnimation.Children.Add(animation);
                }

                if (BusyAnimation.CanFreeze)
                {
                    BusyAnimation.Freeze();
                }

                if (State == ProgressState.Busy && IsEnabled)
                {
                    BusyAnimation.Begin(this, Template, true);
                    IsBusyAnimationRunning = true;
                }
            }
        }
    }
} ;