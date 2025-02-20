using MagicConch.Behavior;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MagicConch.Views.Title
{
    public partial class BackgroundImages : UserControl
    {
        private Point mousePos;
        private Dictionary<Image, Point> originalPositions = new Dictionary<Image, Point>();
        private const double AttractionRadius = 50; // 마우스 영향 반경
        private const double MoveSpeed = 0.025; // 부드러운 이동 속도

        private readonly List<Image> floatingImages = new();

        private List<string> imageSources = new List<string>
        {
            "/MagicConch;component/Assets/Images/Bubble_1.png",
            "/MagicConch;component/Assets/Images/Bubble_2.png",
            "/MagicConch;component/Assets/Images/Bubble_3.png",
            "/MagicConch;component/Assets/Images/Bubble_4.png",
        };

        #region positions
        private Dictionary<Image, Point> images = new();

        private List<Point> Bubble1Position = new List<Point>
        {
            new Point(51.75, 2.58),
            new Point(2.40, 1.63),
            new Point(1.21, 4.66),
        };

        private readonly List<Point> Bubble2Position = new List<Point>
        {
            new Point(8.62, 1.46),
            new Point(1.11, 2.05),
        };

        private readonly List<Point> Bubble3Position = new List<Point>
        {
            new Point(3.79, 1.74),
            new Point(3.14, 11.61),
            new Point(2.71, 2.82),
            new Point(1.96, 1.25),
            new Point(1.44, 1.70),
            new Point(1.22, 1.26),
        };

        private readonly List<Point> Bubble4Position = new List<Point>
        {
            new Point(3.331, 1.81),
            new Point(3.29, 13.76),
            new Point(1.36, 1.52),
        };
        #endregion

        public BackgroundImages()
        {
            this.InitializeComponent();

            CompositionTarget.Rendering += UpdateImages;
            canvas.MouseMove += BackgroundImages_MouseMove;
            Loaded += BackgroundImages_Loaded;
            canvas.SizeChanged += BackgroundImages_SizeChanged;
        }

        private void AddImages(string source, List<Point> positions, Point size)
        {
            foreach (var item in positions)
            {
                var image = CreateFloatingImage(source, size);
                images.Add(image, item);
                canvas.Children.Add(image);
                floatingImages.Add(image);
            }
        }

        private void BackgroundImages_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (var item in images)
            {
                var left = Application.Current.MainWindow.ActualWidth / item.Value.X;
                var top = Application.Current.MainWindow.ActualHeight / item.Value.Y;
                Canvas.SetLeft(item.Key, left);
                Canvas.SetTop(item.Key, top);

                originalPositions[item.Key] = new Point(left, top);
            }
        }

        private void BackgroundImages_Loaded(object sender, RoutedEventArgs e)
        {
            AddImages(imageSources[0], Bubble1Position, new Point(9.6, 4.75));
            AddImages(imageSources[1], Bubble2Position, new Point(16, 7.91));
            AddImages(imageSources[2], Bubble3Position, new Point(29.53, 14.61));
            AddImages(imageSources[3], Bubble4Position, new Point(80, 39.58));

        }

        private Image CreateFloatingImage(string source, Point size)
        {
            var img = new Image
            {
                Source = new BitmapImage(new System.Uri(source, System.UriKind.Relative)),
                IsHitTestVisible = false
            };

            // Application.Current.Resources로 컨버터 가져오기
            var converter = Application.Current.Resources["PercentSizeConverter"] as IValueConverter;

            if (converter != null)
            {
                // Width 바인딩
                var widthBinding = new Binding("ActualWidth")
                {
                    Source = Application.Current.MainWindow,
                    Converter = converter,
                    ConverterParameter = size.X
                };
                img.SetBinding(WidthProperty, widthBinding);

                // Height 바인딩
                var heightBinding = new Binding("ActualHeight")
                {
                    Source = Application.Current.MainWindow,
                    Converter = converter,
                    ConverterParameter = size.Y
                };
                img.SetBinding(HeightProperty, heightBinding);
            }

            // 애니메이션 Behavior 추가
            var behavior = new FloattingAnimationBehavior();
            Interaction.GetBehaviors(img).Add(behavior);

            return img;
        }

        private void UpdateImages(object? sender, EventArgs e)
        {
            foreach (var child in canvas.Children)
            {
                if (child is Image img)
                {
                    MoveImage(img);
                }
            }
        }

        private void BackgroundImages_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos = e.GetPosition(canvas);
        }

        private void MoveImage(Image img)
        {
            double imgX = Canvas.GetLeft(img);
            double imgY = Canvas.GetTop(img);
            double centerX = imgX + img.Width / 2;
            double centerY = imgY + img.Height / 2;
            double dx = mousePos.X - centerX;
            double dy = mousePos.Y - centerY;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            double radius = Math.Sqrt((img.Width / 2) * (img.Width / 2) + (img.Height / 2) * (img.Height / 2)) + 20;

            Point originalPos = originalPositions[img];
            double targetX, targetY;
            
            Console.WriteLine(distance);
            if (distance < radius) // 마우스 가까이 있을 때
            {
                targetX = imgX + dx * MoveSpeed;
                targetY = imgY + dy * MoveSpeed;
            }
            else // 멀어지면 원래 자리로 복귀
            {
                targetX = imgX + (originalPos.X - imgX) * MoveSpeed;
                targetY = imgY + (originalPos.Y - imgY) * MoveSpeed;
            }

            Canvas.SetLeft(img, targetX);
            Canvas.SetTop(img, targetY);
        }
    }
}
