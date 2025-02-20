using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace MagicConch.Behavior
{
    internal class FloattingAnimationBehavior : Behavior<UIElement>
    {
        protected override async void OnAttached()
        {
            await SetStoryBoard();
        }

        protected override void OnDetaching()
        {

        }

        private async Task SetStoryBoard()
        {
            Random random = new Random();

            TimeSpan duration = TimeSpan.FromMilliseconds(random.Next(1200,3000));

            await Task.Delay(random.Next(0, 2000));

            TranslateTransform translateTransform = new TranslateTransform();

            AssociatedObject.RenderTransform = translateTransform;

            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();

            animation.KeyFrames.Add(new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.Zero))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
            });

            // 1초 후: 위로 이동 (Y = -50)
            animation.KeyFrames.Add(new EasingDoubleKeyFrame(-6.5, KeyTime.FromTimeSpan(duration))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
            });

            // 2초 후: 다시 내려옴 (Y = 0)
            animation.KeyFrames.Add(new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(duration.Add(duration)))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
            });

            var storyboard = new Storyboard();

            storyboard.RepeatBehavior = RepeatBehavior.Forever;

            Storyboard.SetTarget(animation, translateTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath(TranslateTransform.YProperty));

            storyboard.Children.Add(animation);

            storyboard.Begin();
        }
    }
}
