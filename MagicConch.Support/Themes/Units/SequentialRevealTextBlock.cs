﻿using MagicConch.Support.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MagicConch.Support.Themes.Units
{
    public partial class SequentialRevealTextBlock : Control
    {
        private List<TextBlock> Labels = new List<TextBlock>();
        private TextBlock textBlock;
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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            textBlock = GetTemplateChild("AnimationTextBlock") as TextBlock;
            stackPanel = GetTemplateChild("stackPanel") as StackPanel;
        }

        public void StartAnimation()
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

            timer.Tick += SequentialAnimation;
            timer.Interval = TimeSpan.FromMilliseconds(60);
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
            var transformAnimation = new DoubleAnimation
            {
                From = Offset,
                To = 0,
                Duration = this.Duration,
                BeginTime = TimeSpan.FromMilliseconds(0),
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(transformAnimation, transform);
            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("Y"));

            var storyboard = new Storyboard();
            storyboard.Children.Add(transformAnimation);
            storyboard.Begin();
        }
    }
}
