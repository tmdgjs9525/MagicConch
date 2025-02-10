using MagicConch.Support.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MagicConch.Support.Themes.Units
{
    public partial class SequentialRevealButton : Button, IAnimation
    {
        private TranslateTransform translateTransform;
        private StackPanel stackPanel;
        private Border underLine;
        private int currentIndex = 0;
        private DispatcherTimer timer = new DispatcherTimer();

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(SequentialRevealButton),
                new PropertyMetadata(string.Empty));

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(SequentialRevealButton), new PropertyMetadata(new TimeSpan(0,0,0,1,6)));
        public int StartDelay
        {
            get { return (int)GetValue(StartDelayProperty); }
            set { SetValue(StartDelayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartDelay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartDelayProperty =
            DependencyProperty.Register("StartDelay", typeof(int), typeof(SequentialRevealButton), new PropertyMetadata(0));

        public int Delay
        {
            get { return (int)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Delay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.Register("Delay", typeof(int), typeof(SequentialRevealButton), new PropertyMetadata(55));

        public int Offset
        {
            get { return (int)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Offset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(int), typeof(SequentialRevealButton), new PropertyMetadata(30));

        static SequentialRevealButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SequentialRevealButton), new FrameworkPropertyMetadata(typeof(SequentialRevealButton)));
        }

        public SequentialRevealButton()
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
                From = translateTransform.X,
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
            underLine.Width = ActualWidth;
            translateTransform.X = -ActualWidth;

            Offset = (int)ActualHeight;

            var rect = new RectangleGeometry();
            rect.Rect = new Rect(0, 0, ActualWidth, ActualHeight);
            this.Clip = rect;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            translateTransform = GetTemplateChild("underlineTransform") as TranslateTransform;

            stackPanel = GetTemplateChild("stackPanel") as StackPanel;
            underLine = GetTemplateChild("underline") as Border;            
        }

        public async Task StartAnimation()
        {
            var stringList = Text.Select(s => s.ToString()).ToList();

            for (int i = 0; i < stringList.Count; i++)
            {
                var textBlock = new TextBlock
                {
                    Text = stringList[i],
                    FontSize = FontSize,
                    Foreground = Foreground,
                    FontWeight = FontWeight,
                    Opacity = 0,
                };

                stackPanel.Children.Add(textBlock);
            }

            await Task.Delay(StartDelay);

            timer.Tick += SequentialAnimation;
            timer.Interval = TimeSpan.FromMilliseconds(Delay);
            timer.Start();
        }

        private void SequentialAnimation(object sender, EventArgs e)
        {
            var textblock = stackPanel.Children[currentIndex++] as TextBlock;
            textblock.Opacity = 1;
            AnimateText(textblock);

            if (currentIndex >= stackPanel.Children.Count)
            {
                timer.Stop();
                currentIndex = 0;
            }
        }

        private void AnimateText(FrameworkElement element)
        {
            var transform = new TranslateTransform { Y = Offset };
            element.RenderTransform = transform;

            double to = ActualHeight * 0.1;

            var transformAnimation = new DoubleAnimation
            {
                From = Offset,
                To = 0,
                Duration = this.Duration,
                BeginTime = TimeSpan.FromMilliseconds(0),
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseInOut}
            };

            Storyboard.SetTarget(transformAnimation, transform);
            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("Y"));

            var storyboard = new Storyboard();
            storyboard.Children.Add(transformAnimation);

            storyboard.Begin();
        }
    }
}
