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
        private Viewbox PART_AlarmClockViewBox = null!;
        private Storyboard shirnkStoryboard = new Storyboard();
        private Storyboard expandStoryboard = new Storyboard();

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

            PART_AlarmClockViewBox = (Viewbox)GetTemplateChild("PART_AlarmClockViewBox");

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
            DoubleAnimation heightDoubleAnimation = new DoubleAnimation()
            {
                Duration = Duration,
                EasingFunction = new SineEase() { EasingMode = EasingMode.EaseOut },
                From = ActualHeight,
                To = ActualHeight / 2,
            };

            DoubleAnimation widthDoubleAnimation = new DoubleAnimation()
            {
                Duration = Duration,
                EasingFunction = new SineEase() { EasingMode = EasingMode.EaseOut },
                From = ActualWidth,
                To = ActualWidth * 1.5,
            };

            Storyboard.SetTarget(heightDoubleAnimation, PART_AlarmClockViewBox);
            Storyboard.SetTargetProperty(heightDoubleAnimation, new PropertyPath(Viewbox.HeightProperty));

            Storyboard.SetTarget(widthDoubleAnimation, PART_AlarmClockViewBox);
            Storyboard.SetTargetProperty(widthDoubleAnimation, new PropertyPath(Viewbox.WidthProperty));

            shirnkStoryboard.Children.Add(heightDoubleAnimation);
            shirnkStoryboard.Children.Add(widthDoubleAnimation);

            shirnkStoryboard.Completed += (s, e) =>
            {
                expandStoryboard.Begin();
            };
        }

        private void setExpandAnimation()
        {
            DoubleAnimation heightDoubleAnimation = new DoubleAnimation()
            {
                Duration = Duration,
                EasingFunction = new ElasticEase() { EasingMode = EasingMode.EaseOut },
                From = ActualHeight / 2,
                To = ActualHeight,
            };

            DoubleAnimation widthDoubleAnimation = new DoubleAnimation()
            {
                Duration = Duration,
                EasingFunction = new ElasticEase() { EasingMode = EasingMode.EaseOut },
                From = ActualWidth * 1.5,
                To = ActualWidth,
            };

            Storyboard.SetTarget(heightDoubleAnimation, PART_AlarmClockViewBox);
            Storyboard.SetTargetProperty(heightDoubleAnimation, new PropertyPath(Viewbox.HeightProperty));

            Storyboard.SetTarget(widthDoubleAnimation, PART_AlarmClockViewBox);
            Storyboard.SetTargetProperty(widthDoubleAnimation, new PropertyPath(Viewbox.WidthProperty));

            expandStoryboard.Children.Add(heightDoubleAnimation);
            expandStoryboard.Children.Add(widthDoubleAnimation);
        }
    }
}
