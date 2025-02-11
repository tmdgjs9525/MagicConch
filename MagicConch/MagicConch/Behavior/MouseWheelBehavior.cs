using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows;

namespace MagicConch.Behavior
{
    public class MouseWheelBehavior : Behavior<UIElement>
    {
        public ICommand MouseWheelCommand
        {
            get { return (ICommand)GetValue(MouseWheelCommandProperty); }
            set { SetValue(MouseWheelCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseWheelCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseWheelCommandProperty =
            DependencyProperty.Register("MouseWheelCommand", typeof(ICommand), typeof(MouseWheelBehavior), new PropertyMetadata(null));

        protected override void OnAttached()
        {
            AssociatedObject.MouseWheel += OnMouseWheel;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseWheel -= OnMouseWheel;
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (MouseWheelCommand?.CanExecute(e) == true)
            {
                MouseWheelCommand.Execute(e);
            }
        }
    }
}
