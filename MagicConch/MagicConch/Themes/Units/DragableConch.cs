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
    public partial class DragableConch : Control
    {
        private Canvas ConchCanvas = null!;
        private Image Part_Body = null!;
        private Border Part_Handle = null!;
        private Point offset;  
        private bool isPressed = false;

        private Line line = null!;
        static DragableConch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DragableConch), new FrameworkPropertyMetadata(typeof(DragableConch)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ConchCanvas = (Canvas)GetTemplateChild("PART_ConchCanvas");
            Part_Body = (Image)GetTemplateChild("PART_Body");
            Part_Handle = (Border)GetTemplateChild("PART_Handle");

            Part_Handle.MouseLeftButtonDown += Part_Handle_MouseLeftButtonDown;
            Part_Handle.MouseLeftButtonUp += Part_Handle_MouseLeftButtonUp;
            Part_Handle.MouseMove += Part_Handle_MouseMove;

            Canvas.SetZIndex(Part_Body, 0);
            Canvas.SetZIndex(Part_Handle, 2);

            SizeChanged += DragableConch_SizeChanged;
        }

        private void DragableConch_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double x = Canvas.GetLeft(Part_Handle) + 17;
            double y = Canvas.GetTop(Part_Handle) + Part_Handle.ActualHeight -20;

            line = new Line() { X1 = x, Y1 = y, X2 = x, Y2 = y, Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = 2 };

            ConchCanvas.Children.Add(line);
            Canvas.SetZIndex(line, 1);
        }

        private void Part_Handle_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isPressed = false;
            Part_Handle?.ReleaseMouseCapture();
        }

        private void Part_Handle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isPressed = true;
            offset = e.GetPosition(Part_Handle);
            Part_Handle?.CaptureMouse();
        }

        private void Part_Handle_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isPressed is true)
            {
                Point point = e.GetPosition(ConchCanvas);
                //Thickness margin = new Thickness();
                //margin.Left = point.X - _offset.X;
                //margin.Top = point.Y - _offset.Y;

                //Part_Handle.Margin = margin;

                double x = point.X - offset.X;
                double y = point.Y - offset.Y;

                Canvas.SetLeft(Part_Handle, x);
                Canvas.SetTop(Part_Handle, y);

                line.X2 = x + 10;
                line.Y2 = y + Part_Handle.ActualHeight - 10;
            }
        }
    }
}
