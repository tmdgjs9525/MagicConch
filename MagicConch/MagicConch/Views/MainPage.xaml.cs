using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace MagicConch.Views
{
    public partial class MainPage : Page
    {
        private DateTime lastClickTime = DateTime.MinValue;
        private int clickCount = 0;
        private readonly TimeSpan doubleClickThreshold = TimeSpan.FromMilliseconds(700);

        public MainPage()
        {
            this.InitializeComponent();

            MouseRightButtonDown += MainPage_MouseRightButtonDown;
            KeyDown += MainPage_KeyDown;
        }

        private void MainPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            { // Ctrl + A 입력을 차단
                e.Handled = true; 
            }
        }

        private void MainPage_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
