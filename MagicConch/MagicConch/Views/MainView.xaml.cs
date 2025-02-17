using MagicConch.Helper;
using MagicConch.Support.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace MagicConch.Views
{
    public partial class MainView : UserControl
    {
        private List<IAnimation> animationControls = new List<IAnimation>();

        public MainView()
        {
            this.InitializeComponent();

            //VisualHelper.FindAllAnimationTextBlock(body.Content, animationControls);
            VisualHelper.FindAllAnimationTextBlock(header.Content, animationControls);
            VisualHelper.FindAllAnimationTextBlock(this.Content, animationControls);

            Loaded += MainView_Loaded;
        }

        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                foreach (IAnimation control in animationControls)
                {
                    control.StartAnimation();
                }
            }));
        }
    }
}
