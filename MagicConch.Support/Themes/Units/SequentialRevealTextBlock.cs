using MagicConch.Support.EaseFunctions;
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
    public partial class SequentialRevealTextBlock : Control, IAnimation
    {
        private StackPanel stackPanel;
        private int currentIndex = 0;
        private DispatcherTimer timer = new DispatcherTimer();

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(SequentialRevealTextBlock),
                new PropertyMetadata(string.Empty));

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(SequentialRevealTextBlock), new PropertyMetadata(new TimeSpan(0,0,1)));



        public int Delay
        {
            get { return (int)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Delay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.Register("Delay", typeof(int), typeof(SequentialRevealTextBlock), new PropertyMetadata(40));



        public int StartDelay
        {
            get { return (int)GetValue(StartDelayProperty); }
            set { SetValue(StartDelayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartDelay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartDelayProperty =
            DependencyProperty.Register("StartDelay", typeof(int), typeof(SequentialRevealTextBlock), new PropertyMetadata(0));


        public int Offset
        {
            get { return (int)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Offset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(int), typeof(SequentialRevealTextBlock), new PropertyMetadata(30));

        static SequentialRevealTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SequentialRevealTextBlock), new FrameworkPropertyMetadata(typeof(SequentialRevealTextBlock)));
        }

        public SequentialRevealTextBlock()
        {
            SizeChanged += SequentialRevealTextBlock_SizeChanged;    
        }

        private void SequentialRevealTextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var rect = new RectangleGeometry();
            rect.Rect = new Rect(0, 0, ActualWidth, Height);
            this.Clip = rect;

            stackPanel.Children.Cast<TextBlock>().ToList().ForEach(child => { child.FontSize = FontSize; });
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            stackPanel = GetTemplateChild("stackPanel") as StackPanel;

            var splitedText = Text.Select(s => s.ToString()).ToList();

            for (int i = 0; i < splitedText.Count; i++)
            {
                var textBlock = new TextBlock
                {
                    Text = splitedText[i],
                    FontSize = FontSize,
                    Foreground = Foreground,
                    VerticalAlignment = VerticalAlignment,
                    HorizontalAlignment = HorizontalAlignment,
                    FontWeight = FontWeight,
                    IsHitTestVisible = false,
                    Opacity = 0,
                };
                stackPanel.Children.Add(textBlock);
            }

        }

        public async Task StartAnimation()
        {
            await Task.Delay(StartDelay);

            timer.Tick += sequentialAnimation;
            timer.Interval = TimeSpan.FromMilliseconds(Delay);
            timer.Start();
        }

        private void sequentialAnimation(object sender, EventArgs e)
        {
            var textblock = stackPanel.Children[currentIndex++] as TextBlock;

            if (string.IsNullOrWhiteSpace(textblock.Text) is false)
            {
                textblock.Opacity = 1;
                animateText(textblock);
            }

            if (currentIndex >= stackPanel.Children.Count)
            {
                timer.Tick -= sequentialAnimation;
                timer.Stop();
                currentIndex = 0;
            }
        }

        private void animateText(FrameworkElement element)
        {
            var transform = new TranslateTransform { Y = Offset };
            element.RenderTransform = transform;

            var transformAnimation = new DoubleAnimation
            {
                From = Offset,
                To = 0,
                Duration = Duration,
                BeginTime = TimeSpan.FromMilliseconds(0),
                EasingFunction = new CustomExponentialEase { EasingMode = EasingMode.EaseOut}
            };

            Storyboard.SetTarget(transformAnimation, transform);
            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("Y"));

            var storyboard = new Storyboard();
            storyboard.Children.Add(transformAnimation);

            storyboard.Begin();
        }
    }
}
