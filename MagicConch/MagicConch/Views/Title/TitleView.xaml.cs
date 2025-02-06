using MagicConch.Support.Themes.Units;
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
        private List<SequentialRevealTextBlock> animationTextBlocks = new List<SequentialRevealTextBlock>();
        public TitleView()
        {
            this.InitializeComponent();

            FindAllAnimationTextBlock(grid);

            Loaded += TitleView_Loaded;
        }

        private void TitleView_Loaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            var a = test;

            CompositionTarget.Rendering -= CompositionTarget_Rendering;

            foreach (SequentialRevealTextBlock block in animationTextBlocks)
            {
                block.StartAnimation();
            }
        }

        public void FindAllAnimationTextBlock(DependencyObject parent)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is SequentialRevealTextBlock)
                {
                    var textblock = (SequentialRevealTextBlock)child;
                    
                    animationTextBlocks.Add(textblock);
                }

                // 자식이 또 다른 컨트롤을 가지고 있을 수 있으므로 재귀적으로 호출
                FindAllAnimationTextBlock(child);
            }
        }
    }
}
