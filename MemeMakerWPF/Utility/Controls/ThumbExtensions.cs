using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MemeMakerWPF.Utility.Controls
{
    public class MoveThumb : Thumb
    {
        public MoveThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement designerItem = this.DataContext as FrameworkElement;

            //Element inside ItemsControl are wrapped inside ContentPresenter
            var contentPresenter = VisualTreeHelper.GetParent(designerItem);
            if (contentPresenter != null && contentPresenter is ContentPresenter)
                designerItem = contentPresenter as FrameworkElement;

            if (designerItem != null)
            {
                double left = Canvas.GetLeft(designerItem);
                double top = Canvas.GetTop(designerItem);

                Canvas.SetLeft(designerItem, left + e.HorizontalChange);
                Canvas.SetTop(designerItem, top + e.VerticalChange);
            }
        }
    }

    public class ResizeThumb : Thumb
    {
        public ResizeThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement designerItem = this.DataContext as FrameworkElement;

            bool isContentPresenter = false;
            FrameworkElement contentPresenterFram = new FrameworkElement();

            //Element inside ItemsControl are wrapped inside ContentPresenter
            var contentPresenter = VisualTreeHelper.GetParent(designerItem);
            isContentPresenter = (contentPresenter != null && contentPresenter is ContentPresenter);

            if (isContentPresenter)
                contentPresenterFram = contentPresenter as FrameworkElement;

            if (designerItem != null)
            {
                double deltaVertical, deltaHorizontal;

                switch (VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        {
                            deltaVertical = Math.Min(-e.VerticalChange, designerItem.ActualHeight - designerItem.MinHeight);
                            designerItem.Height -= deltaVertical;
                        }
                        break;
                    case VerticalAlignment.Top:
                        {
                            deltaVertical = Math.Min(e.VerticalChange, designerItem.ActualHeight - designerItem.MinHeight);
                            if (isContentPresenter)
                                Canvas.SetTop(contentPresenterFram, Canvas.GetTop(contentPresenterFram) + deltaVertical);
                            else
                                Canvas.SetTop(designerItem, Canvas.GetTop(designerItem) + deltaVertical);
                            designerItem.Height -= deltaVertical;
                        }
                        break;
                    default:
                        break;
                }

                switch (HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        {
                            deltaHorizontal = Math.Min(e.HorizontalChange, designerItem.ActualWidth - designerItem.MinWidth);
                            if (isContentPresenter)
                                Canvas.SetLeft(contentPresenterFram, Canvas.GetLeft(contentPresenterFram) + deltaHorizontal);
                            else
                                Canvas.SetLeft(designerItem, Canvas.GetLeft(designerItem) + deltaHorizontal);
                            designerItem.Width -= deltaHorizontal;
                        }
                        break;
                    case HorizontalAlignment.Right:
                        {
                            deltaHorizontal = Math.Min(-e.HorizontalChange, designerItem.ActualWidth - designerItem.MinWidth);
                            designerItem.Width -= deltaHorizontal;
                        }
                        break;
                    default:
                        break;
                }
            }

            e.Handled = true;
        }
    }
}
