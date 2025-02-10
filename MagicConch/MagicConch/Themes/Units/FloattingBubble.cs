using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MagicConch.Themes.Units
{
    public partial class FloattingBubble : Control
    {
        private TranslateTransform? translateTransform;
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
            var transformAnimation = new DoubleAnimation
            {
                From = -ActualWidth,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(500),
                BeginTime = TimeSpan.FromMilliseconds(0),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(transformAnimation, translateTransform);
            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("X"));

            var storyboard = new Storyboard();
            storyboard.Children.Add(transformAnimation);

            storyboard.Begin();
        }

        private void SequentialRevealButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var transformAnimation = new DoubleAnimation
            {
                From = translateTransform?.X,
                To = ActualWidth,
                Duration = TimeSpan.FromMilliseconds(500),
                BeginTime = TimeSpan.FromMilliseconds(0 ),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(transformAnimation, translateTransform);
            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("X"));

            var storyboard = new Storyboard();
            storyboard.Children.Add(transformAnimation);

            storyboard.Begin();
        }

        private void SequentialRevealButton_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //ArgumentNullException.ThrowIfNull(translateTransform);

            translateTransform.X = -ActualWidth;

            Offset = (int)ActualHeight;

            var rect = new RectangleGeometry();
            rect.Rect = new Rect(0, 0, ActualWidth, ActualHeight);
            this.Clip = rect;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            translateTransform = (TranslateTransform)GetTemplateChild("underlineTransform");          
        }

        public async Task StartAnimation()
        {
            await Task.Delay(StartDelay);

            timer.Tick += Animation!;
            timer.Interval = TimeSpan.FromMilliseconds(Delay);
            timer.Start();
        }

        private void Animation(object sender, EventArgs e)
        {

        }
    }
}
