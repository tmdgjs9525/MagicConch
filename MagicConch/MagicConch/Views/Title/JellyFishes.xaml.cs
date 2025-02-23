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
    public class ImageInfo
    {
        public Image? Image { get; set; }

        public string Source { get; set; } = string.Empty;

        public Size size { get; set; }

        public Point point { get; set; }
    }

    public partial class JellyFishes : UserControl
    {
        private List<ImageInfo> imageInfos = new List<ImageInfo>
        {
            //new ImageInfo
            //{
            //    Source = "/MagicConch;component/Assets/Images/JellyFish_1.png" ,
            //    size = new Size(325,350),
            //    point = new Point(169,257.9),
            //},
            new ImageInfo
            {
                Source = "/MagicConch;component/Assets/Images/JellyFish_2.png" ,
                size = new Size(170,170),
                point = new Point(590,675.5),
            },
            new ImageInfo
            {
                Source = "/MagicConch;component/Assets/Images/JellyFish_3.png" ,
                size = new Size(140,140),
                point = new Point(1167.7,580),
            },
            new ImageInfo
            {
                Source = "/MagicConch;component/Assets/Images/JellyFish_4.png" ,
                size = new Size(210,210),
                point = new Point(1583.4,619.7),
            },
            new ImageInfo
            {
                Source = "/MagicConch;component/Assets/Images/JellyFish_5.png" ,
                size = new Size(140,140),
                point = new Point(1287.1,36.6),
            },
            //new ImageInfo
            //{
            //    Source = "/MagicConch;component/Assets/Images/JellyFish_6.png" ,
            //    size = new Size(480,480),
            //    point = new Point(212.5,434.7),
            //},
            //new ImageInfo
            //{
            //    Source = "/MagicConch;component/Assets/Images/JellyFish_7.png" ,
            //    size = new Size(460,460),
            //    point = new Point(1320,187.9),
            //},
            //new ImageInfo
            //{
            //    Source = "/MagicConch;component/Assets/Images/JellyFish_8.png" ,
            //    size = new Size(400,150),
            //    point = new Point(502.3,0),
            //},
        };

        private Dictionary<Image, Point> originalPositions = new();

        public JellyFishes()
        {
            this.InitializeComponent();
            Loaded += JellyFishes_Loaded;
            SizeChanged += JellyFishes_SizeChanged;
        }

        private void JellyFishes_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (var item in imageInfos)
            {
                if (item.Image is null)
                {
                    return;
                }
                var x = 1920 / item.point.X;
                var y = 950 / item.point.Y;
                var left = Application.Current.MainWindow.ActualWidth / x;
                var top = Application.Current.MainWindow.ActualHeight / y;
                Canvas.SetLeft(item.Image, left);
                Canvas.SetTop(item.Image, top);

                //var point = originalPositions[item.Image];
                //point.X = left; 
                //point.Y = top;
            }
        }

        private void JellyFishes_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in imageInfos)
            {
                Image img = CreateFloatingImage(item.Source, item.size, item.point);
                item.Image = img;
                canvas.Children.Add(img);
            }
        }


        private Image CreateFloatingImage(string source, Size size, Point point)
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
                var a = Application.Current.MainWindow.ActualWidth;
                // Width 바인딩
                var widthBinding = new Binding("ActualWidth")
                {
                    Source = Application.Current.MainWindow,
                    Converter = converter,
                    ConverterParameter = 1920 / size.Width
                };
                img.SetBinding(WidthProperty, widthBinding);

                // Height 바인딩
                var heightBinding = new Binding("ActualHeight")
                {
                    Source = Application.Current.MainWindow,
                    Converter = converter,
                    ConverterParameter = 950 / size.Height
                };
                img.SetBinding(HeightProperty, heightBinding);
            }

            // 애니메이션 Behavior 추가
            var behavior = new FloattingAnimationBehavior();
            Interaction.GetBehaviors(img).Add(behavior);

            return img;
        }
    }
}
