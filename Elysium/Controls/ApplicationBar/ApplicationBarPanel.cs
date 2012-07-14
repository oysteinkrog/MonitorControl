using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using JetBrains.Annotations;

namespace Elysium.Controls
{
    [PublicAPI]
    public class ApplicationBarPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            var infinitySize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            var desiredSize = new Size(0, 0);

            Contract.Assume(InternalChildren != null);
            var children = InternalChildren.Cast<UIElement>().ToList();
            foreach (var child in children.Where(child => child != null))
            {
                // NOTE: Code Contracts doesn't support closures
                Contract.Assume(child != null);
                child.Measure(infinitySize);
                // NOTE: Lack of contracts: DesiredSize.Width is non-negative
                Contract.Assume(child.DesiredSize.Width >= 0d);
                desiredSize.Width = Math.Max(desiredSize.Width, child.DesiredSize.Width);
                desiredSize.Height = Math.Max(desiredSize.Height, child.DesiredSize.Height);
            }

            desiredSize.Width *= children.Count();

            foreach (var child in InternalChildren.Cast<UIElement>().Where(child => child != null))
            {
                // NOTE: Code Contracts doesn't support closures
                Contract.Assume(child != null);
                child.Measure(desiredSize);
            }

            return desiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var children = InternalChildren.Cast<UIElement>().ToList();
            var leftDocked = children.Where(child => ApplicationBar.GetDock(child) == ApplicationBarDock.Left).ToArray();
            var rightDocked = children.Where(child => ApplicationBar.GetDock(child) == ApplicationBarDock.Right).Reverse().ToArray();

            children = new List<UIElement>();
            var bigger = leftDocked.Count() > rightDocked.Count() ? leftDocked : rightDocked;
            var smaller = leftDocked != bigger ? leftDocked : rightDocked;

            for (var index = 0; index < bigger.Count(); index++)
            {
                children.Add(bigger[index]);
                if (index < smaller.Count())
                {
                    children.Add(smaller[index]);
                }
                
            }

            var maxWidth = 0d;

            for (var index = 0; index < children.Count; index++)
            {
                var child = children[index];
                var tempMaxWidth = Math.Max(maxWidth, child.DesiredSize.Width);
                if (tempMaxWidth * (index + 1) <= finalSize.Width)
                {
                    maxWidth = tempMaxWidth;
                }
                else break;
            }

            var leftFilledWidth = 0d;
            var rightFilledWidth = 0d;
            foreach (var child in children.Where(child => child != null))
            {
                var isRightDocked = ApplicationBar.GetDock(child) == ApplicationBarDock.Right;
                var x = !isRightDocked ? leftFilledWidth : finalSize.Width - rightFilledWidth - maxWidth;

                if (!isRightDocked)
                {
                    leftFilledWidth += maxWidth;
                }
                else
                {
                    rightFilledWidth += maxWidth;
                }

                child.Arrange(leftFilledWidth + rightFilledWidth <= finalSize.Width
                                  ? new Rect(new Point(x, 0), new Size(maxWidth, finalSize.Height))
                                  : new Rect(new Point(0, 0), new Size(0, 0)));
            }
            return finalSize;
        }
    }
} ;