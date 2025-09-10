using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MagicConch.Themes.Units
{
    public class ToolTipUnderLineButton : Button
    {
        Popup Part_Popup = null!;
        Button Part_UnderLineButton = null!;




        public string ToolTipText
        {
            get { return (string)GetValue(ToolTipTextProperty); }
            set { SetValue(ToolTipTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ToolTipText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToolTipTextProperty =
            DependencyProperty.Register("ToolTipText", typeof(string), typeof(ToolTipUnderLineButton), new PropertyMetadata(string.Empty));


        static ToolTipUnderLineButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolTipUnderLineButton), new FrameworkPropertyMetadata(typeof(ToolTipUnderLineButton)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Part_Popup = (Popup)GetTemplateChild("PART_Popup");
            Part_UnderLineButton = (Button)GetTemplateChild("PART_UnderLineButton");

            Part_UnderLineButton.Click += Part_UnderLineButton_Click;
        }

        private void Part_UnderLineButton_Click(object sender, RoutedEventArgs e)
        {
            Part_Popup.IsOpen = !Part_Popup.IsOpen;
        }
    }
}
