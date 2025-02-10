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
       
        static DragableConch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DragableConch), new FrameworkPropertyMetadata(typeof(DragableConch)));
        }

        public DragableConch()
        {

        }
 
    }
}
