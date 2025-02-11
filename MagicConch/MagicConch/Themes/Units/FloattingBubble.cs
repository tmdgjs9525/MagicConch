using MagicConch.Support.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Xml.Linq;

namespace MagicConch.Themes.Units
{
    public partial class FloattingBubble : Control, IAnimation
    {
        private TranslateTransform? PART_TranslateTransform;
        private DispatcherTimer timer = new DispatcherTimer();

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(FloattingBubble), new PropertyMetadata(new TimeSpan(0,0,0,1,6)));


        public int StartDelay
        {
            get { return (int)GetValue(StartDelayProperty); }
            set { SetValue(StartDelayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartDelay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartDelayProperty =
            DependencyProperty.Register("StartDelay", typeof(int), typeof(FloattingBubble), new PropertyMetadata(0));


        public int Delay
        {
            get { return (int)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Delay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.Register("Delay", typeof(int), typeof(FloattingBubble), new PropertyMetadata(55));

        public int Offset
        {
            get { return (int)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Offset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(int), typeof(FloattingBubble), new PropertyMetadata(30));

        static FloattingBubble()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloattingBubble), new FrameworkPropertyMetadata(typeof(FloattingBubble)));
        }

        public FloattingBubble()
        {
            SizeChanged += SequentialRevealButton_SizeChanged;
            MouseLeave += SequentialRevealButton_MouseLeave;
            MouseEnter += SequentialRevealButton_MouseEnter;
            
        }

        private void SequentialRevealButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void SequentialRevealButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void SequentialRevealButton_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_TranslateTransform = (TranslateTransform)GetTemplateChild("PART_TranslateTransform");          
        }

        public async Task StartAnimation()
        {
            await Task.Delay(StartDelay);

            Animation();
        }

        private void Animation()
        {
            if (PART_TranslateTransform is null)
            {
                return;
            }

            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation, PART_TranslateTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath(TranslateTransform.YProperty));

            // 1초 후: 위로 이동 (Y = -50)
            animation.KeyFrames.Add(new EasingDoubleKeyFrame(-5, KeyTime.FromTimeSpan(Duration))
            {
                EasingFunction = new BackEase { EasingMode = EasingMode.EaseInOut }
            });

            // 2초 후: 다시 내려옴 (Y = 0)
            animation.KeyFrames.Add(new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(Duration.Add(Duration)))
            {
                EasingFunction = new BackEase { EasingMode = EasingMode.EaseInOut }
            });

            var storyboard = new Storyboard();
            storyboard.RepeatBehavior = RepeatBehavior.Forever;

            storyboard.Children.Add(animation);

            storyboard.Begin();
        }
    }
}
