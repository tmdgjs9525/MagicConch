using MagicConch.Support.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MagicConch.Themes.Units
{
    public partial class Equalizer : Control, IAnimation
    {
        private ItemsControl PART_EQBarItemsControl = null!;
        private Random random = new Random();
        public double BarWidth
        {
            get { return (double)GetValue(BarWidthProperty); }
            set { SetValue(BarWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BarWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BarWidthProperty =
            DependencyProperty.Register("BarWidth", typeof(double), typeof(Equalizer), new PropertyMetadata(15.0));

        public Thickness BarMargin
        {
            get { return (Thickness)GetValue(BarMarginProperty); }
            set { SetValue(BarMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BarMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BarMarginProperty =
            DependencyProperty.Register("BarMargin", typeof(Thickness), typeof(Equalizer), new PropertyMetadata(new Thickness(10,0,0,0)));

        public int BarCount
        {
            get { return (int)GetValue(BarCountProperty); }
            set { SetValue(BarCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BarCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BarCountProperty =
            DependencyProperty.Register("BarCount", typeof(int), typeof(Equalizer), new PropertyMetadata(3));

        static Equalizer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Equalizer), new FrameworkPropertyMetadata(typeof(Equalizer)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_EQBarItemsControl = (ItemsControl)GetTemplateChild("PART_EQBarItemsControl");

            //EQBar가 아래로 좀 튀어나와서 마진수정
            int bottomMargin = 2;
            
            Thickness thickness = BarMargin;

            thickness.Bottom = bottomMargin;

            BarMargin = thickness;

            Random random = new Random();
            for (int i = 0; i < BarCount; i++)
            {
                var border = new Border
                {
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Width = BarWidth,
                    Height = random.NextDouble() * Height,
                    Background = Background,
                    Margin = i == 0 ? new Thickness(0, 0, 0, bottomMargin) : BarMargin,
                };

                PART_EQBarItemsControl.Items.Add(border);
            }
        }

        public async Task StartAnimation()
        {
            await Task.Delay(500);

            foreach (var item in PART_EQBarItemsControl.Items)
            {
                if (item is Border border)
                {
                    Animation(border);
                }
            }
        }

        private void Animation(Border border)
        {
            int maximumDuration = 400;
            int minimumDuration = 200;

            TimeSpan Duration = TimeSpan.FromMilliseconds(random.NextDouble() * maximumDuration + minimumDuration);

            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames();

            doubleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.Zero)));

            // 1초 후: 위로 이동 (Y = -50)
            doubleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(random.NextDouble() * Height, KeyTime.FromTimeSpan(Duration))
            {
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
            });

            // 2초 후: 다시 내려옴 (Y = 0)
            doubleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(Duration.Add(Duration)))
            {
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseIn }
            });

            Storyboard.SetTarget(doubleAnimation, border);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Border.HeightProperty));

            storyboard.Children.Add(doubleAnimation);

            storyboard.Completed += (s, e) =>
            {
                doubleAnimation.KeyFrames[1].Value = random.NextDouble() * Height;
                storyboard.Begin();
            };

            storyboard.Begin();
        }
    }
}
