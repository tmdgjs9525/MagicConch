using MagicConch.Support.Interfaces;
using MagicConch.Support.Themes.Units;
using MagicConch.Themes.Units;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace MagicConch.Views.Title
{
    public partial class TitleView : UserControl
    {
        private List<IAnimation> animationControls = new List<IAnimation>();
        public TitleView()
        {
            this.InitializeComponent();

            FindAllAnimationTextBlock(grid);
            FindAllAnimationTextBlock(header);

            FindLogicalChild(header);

            Loaded += TitleView_Loaded;
        }

        private void TitleView_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                var bubble = new FloattingBubble();
                bubble.Duration = TimeSpan.FromMilliseconds(2000);
                Canvas.SetLeft(bubble, new Random().NextDouble() * 1920);
                Canvas.SetTop(bubble, new Random().NextDouble() * 1080);
                BubbleCanvas.Children.Add(bubble);
            }

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;

            animationControls.AddRange(BubbleCanvas.Children.OfType<IAnimation>());

            foreach (IAnimation control in animationControls)
            {
                control.StartAnimation();
            }
        }

        public IAnimation? FindLogicalChild(DependencyObject parent)
        {
            if (parent == null) return null;

            foreach (var child in LogicalTreeHelper.GetChildren(parent))
            {
                if (child is IAnimation typedChild)
                    animationControls.Add(typedChild);

                if (child is DependencyObject depChild)
                {
                    var result = FindLogicalChild(depChild);
                    if (result != null && result is IAnimation animation)
                        animationControls.Add(animation);
                }
            }
            return null;
        }

        public void FindAllAnimationTextBlock(DependencyObject parent)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is IAnimation)
                {
                    var animatinoControl = (IAnimation)child;
                    
                    animationControls.Add(animatinoControl);
                }

                // 자식이 또 다른 컨트롤을 가지고 있을 수 있으므로 재귀적으로 호출
                FindAllAnimationTextBlock(child);
            }
        }
    }
}
