using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MagicConch.Support.Themes.Units
{
    public partial class UnderLineButton : Button
    {
        private TranslateTransform translateTransform;
        private Storyboard mouseEnterStoryboard;
        private Storyboard mouseLeaveStoryboard;

        static UnderLineButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UnderLineButton), new FrameworkPropertyMetadata(typeof(UnderLineButton)));
        }

        public UnderLineButton()
        {
            SizeChanged += UnderLineButton_SizeChanged;
            MouseLeave += UnderLineButton_MouseLeave;
            MouseEnter += UnderLineButton_MouseEnter;
        }

        private void UnderLineButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            mouseEnterStoryboard.Begin();
        }

        private void UnderLineButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            mouseLeaveStoryboard.Begin();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            translateTransform = GetTemplateChild("underlineTransform") as TranslateTransform;

            //SizeChanged += UnderLineButton_SizeChanged;
        }

        private void UnderLineButton_SizeChanged(object sender, RoutedEventArgs e)
        {
            //하단 border가 컨트롤 바깥까지 보이지 않도록 Clip
            translateTransform.X = -ActualWidth;

            var rect = new RectangleGeometry();
            rect.Rect = new Rect(0, 0, ActualWidth, ActualHeight);
            this.Clip = rect;

            setStoryboards();
        }

        private void setStoryboards()
        {
            //Mouse Enter
            var enterAnimation = new DoubleAnimation
            {
                From = -ActualWidth,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(500),
                BeginTime = TimeSpan.FromMilliseconds(0),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(enterAnimation, translateTransform);
            Storyboard.SetTargetProperty(enterAnimation, new PropertyPath("X"));

            mouseEnterStoryboard = new Storyboard();
            mouseEnterStoryboard.Children.Add(enterAnimation);

            //Mouse Leave
            var leaveAnimation = new DoubleAnimation
            {
                //From = translateTransform.X,
                To = ActualWidth,
                Duration = TimeSpan.FromMilliseconds(500),
                BeginTime = TimeSpan.FromMilliseconds(0),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(leaveAnimation, translateTransform);
            Storyboard.SetTargetProperty(leaveAnimation, new PropertyPath("X"));

            mouseLeaveStoryboard = new Storyboard();
            mouseLeaveStoryboard.Children.Add(leaveAnimation);
        }

        
    }
}
