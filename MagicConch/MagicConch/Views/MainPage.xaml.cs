using System.Windows.Controls;

namespace MagicConch.Views
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            MouseRightButtonDown += MainPage_MouseRightButtonDown;
        }

        private void MainPage_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
