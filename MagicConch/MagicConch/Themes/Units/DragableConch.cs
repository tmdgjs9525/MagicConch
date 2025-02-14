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
       
        private bool isPressed = false;

        private Line line = null!;

        /// <summary>
        /// 마우스 클릭 오프셋
        /// </summary>
        private Point offset;
        
        /// <summary>
        /// 손잡이 이미지 끝부분 좌표 오프셋
        /// </summary>
        private readonly int leftOffset = 3;
        /// <summary>
        /// 손잡이 이미지 끝부분 좌표 오프셋
        /// </summary>
        private readonly int bottomOffset = -17;
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

            Canvas.SetTop(Part_Handle, 186);
            Canvas.SetLeft(Part_Handle, 558);
            Canvas.SetZIndex(Part_Handle, 2);

            SizeChanged += DragableConch_SizeChanged;
        }

        private void DragableConch_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double x = Canvas.GetLeft(Part_Handle) + leftOffset;
            double y = Canvas.GetTop(Part_Handle) + Part_Handle.ActualHeight + bottomOffset;

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

                double x = point.X - offset.X;
                double y = point.Y - offset.Y;

                Canvas.SetLeft(Part_Handle, point.X - offset.X);
                Canvas.SetTop(Part_Handle, point.Y - offset.Y);

                line.X2 = x + leftOffset;
                line.Y2 = y + Part_Handle.ActualHeight + bottomOffset;
            }
        }
    }
}
