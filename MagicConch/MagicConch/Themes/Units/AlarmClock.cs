using MagicConch.Helper;
using MagicConch.Support.EaseFunctions;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MagicConch.Themes.Units
{
    public partial class AlarmClock : Button
    {
        private Image PART_AlarmClockViewBox = null!;
        private Storyboard shirnkStoryboard = new Storyboard();
        private Storyboard expandStoryboard = new Storyboard();
        private MediaElement PART_MediaElement = null!;

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(AlarmClock), new PropertyMetadata(TimeSpan.FromMilliseconds(500)));



        static AlarmClock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AlarmClock), new FrameworkPropertyMetadata(typeof(AlarmClock)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            SizeChanged += AlarmClock_SizeChanged;

            PART_AlarmClockViewBox = (Image)GetTemplateChild("PART_AlarmClockViewBox");
            PART_MediaElement = (MediaElement)GetTemplateChild("PART_MediaElement");

            //PART_MediaElement.Volume = 1;
            //var a= new Uri($"{AppDomain.CurrentDomain.BaseDirectory}Assets\\Sounds\\spongebob-boat-horn.mp3", UriKind.Relative);
            //PART_MediaElement.Source = a;

            Click += AlarmClock_Click;
        }

        private void AlarmClock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            setShirnkAnimation();
            setExpandAnimation();
        }

        private void AlarmClock_Click(object sender, RoutedEventArgs e)
        {
            shirnkStoryboard.Begin();
        }

        private void setShirnkAnimation()
        {
            ScaleTransform scaleTransform = new ScaleTransform();
            PART_AlarmClockViewBox.RenderTransform = scaleTransform;

            DoubleAnimation scaleXAnimation = new DoubleAnimation()
            {
                Duration = Duration,
                EasingFunction = new SineEase() { EasingMode = EasingMode.EaseOut },
                From = 1,
                To = 1.5
            };

            DoubleAnimation scaleYAnimation = new DoubleAnimation()
            {
                Duration = Duration,
                EasingFunction = new SineEase() { EasingMode = EasingMode.EaseOut },
                From = 1,
                To = 0.5
            };

            Storyboard.SetTarget(scaleXAnimation, PART_AlarmClockViewBox);
            Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.ScaleX"));

            Storyboard.SetTarget(scaleYAnimation, PART_AlarmClockViewBox);
            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.ScaleY"));

            shirnkStoryboard.Children.Add(scaleXAnimation);
            shirnkStoryboard.Children.Add(scaleYAnimation);

            shirnkStoryboard.Completed += (s, e) =>
            {
                expandStoryboard.Begin();
                PART_MediaElement.Play();
            };
        }

        private void setExpandAnimation()
        {
            DoubleAnimation scaleXAnimation = new DoubleAnimation()
            {
                Duration = Duration,
                EasingFunction = new ElasticEase() { EasingMode = EasingMode.EaseOut },
                From = 1.5,
                To = 1
            };

            DoubleAnimation scaleYAnimation = new DoubleAnimation()
            {
                Duration = Duration,
                EasingFunction = new ElasticEase() { EasingMode = EasingMode.EaseOut },
                From = 0.5,
                To = 1
            };

            Storyboard.SetTarget(scaleXAnimation, PART_AlarmClockViewBox);
            Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.ScaleX"));

            Storyboard.SetTarget(scaleYAnimation, PART_AlarmClockViewBox);
            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.ScaleY"));

            expandStoryboard.Children.Add(scaleXAnimation);
            expandStoryboard.Children.Add(scaleYAnimation);
        }
    }
}
