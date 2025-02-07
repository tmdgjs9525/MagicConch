using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MagicConch.Support.Themes.Units
{
    public partial class UnderLineButton : Button
    {
        private TranslateTransform translateTransform;
        static UnderLineButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UnderLineButton), new FrameworkPropertyMetadata(typeof(UnderLineButton)));
        }

        public UnderLineButton()
        {
            SizeChanged += UnderLineButton_SizeChanged;

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            translateTransform = GetTemplateChild("underlineTransform") as TranslateTransform;
        }

        private void UnderLineButton_SizeChanged(object sender, RoutedEventArgs e)
        {
            translateTransform.X = -ActualWidth;

            var rect = new RectangleGeometry();
            rect.Rect = new Rect(0, 0, ActualWidth, ActualHeight);
            this.Clip = rect;
        }
    }
}
