using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MagicConch.Themes.Units
{
    public partial class DragableConch : Control
    {
        private Canvas? ConchCanvas;
        private Image? Part_Body;
        private Image? Part_Handle;
        private Point _offset;  
        private bool isPressed = false;

        static DragableConch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DragableConch), new FrameworkPropertyMetadata(typeof(DragableConch)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ConchCanvas = GetTemplateChild("PART_ConchCanvas") as Canvas;
            Part_Body = GetTemplateChild("PART_Body") as Image;
            Part_Handle = GetTemplateChild("PART_Handle") as Image;

            if( Part_Handle is not null)
            {
                Part_Handle.MouseLeftButtonDown += Part_Handle_MouseLeftButtonDown;
                Part_Handle.MouseLeftButtonUp += Part_Handle_MouseLeftButtonUp;
                Part_Handle.MouseMove += Part_Handle_MouseMove;
            }
        }

        private void Part_Handle_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isPressed = false;
            Part_Handle?.ReleaseMouseCapture();
        }

        private void Part_Handle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isPressed = true;
            _offset = e.GetPosition(Part_Handle);
            Part_Handle?.CaptureMouse();
        }

        private void Part_Handle_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isPressed is true)
            {
                Point point = e.GetPosition(ConchCanvas);
                Thickness margin = new Thickness();
                margin.Left = point.X - _offset.X;
                margin.Top = point.Y - _offset.Y;

                Part_Handle!.Margin = margin;

                //Canvas.SetLeft(Part_Handle, point.X - _offset.X);
                //Canvas.SetTop(Part_Handle, point.Y - _offset.Y);
            }
        }
    }
}
