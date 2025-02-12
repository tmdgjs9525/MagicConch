using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace MagicConch.Behavior
{
    public class BlinkBehavior : Behavior<UIElement>
    {
        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(BlinkBehavior), new PropertyMetadata(TimeSpan.FromSeconds(1)));


        protected override void OnAttached()
        {
            SetStoryBoard();
        }

        protected override void OnDetaching()
        {

        }

        private void SetStoryBoard()
        {
            Storyboard blinkStoryboard = new Storyboard();
            blinkStoryboard.RepeatBehavior = RepeatBehavior.Forever;

            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();

            animation.KeyFrames.Add(new EasingDoubleKeyFrame(1.0, KeyTime.FromTimeSpan(TimeSpan.Zero))
            {
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
            });

            animation.KeyFrames.Add(new EasingDoubleKeyFrame(0.0, KeyTime.FromTimeSpan(Duration))
            {
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
            });

            animation.KeyFrames.Add(new EasingDoubleKeyFrame(1.0, KeyTime.FromTimeSpan(Duration.Add(Duration)))
            {
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
            });

            Storyboard.SetTarget(animation, AssociatedObject);
            Storyboard.SetTargetProperty(animation, new PropertyPath(UIElement.OpacityProperty));

            blinkStoryboard.Children.Add(animation);

            blinkStoryboard.Begin();
        }
    }
}
