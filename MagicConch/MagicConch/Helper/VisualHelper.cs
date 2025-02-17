using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using MagicConch.Support.Interfaces;

namespace MagicConch.Helper
{
    internal class VisualHelper
    {
        public static List<T> FindVisualChildren<T>(DependencyObject obj) where T : DependencyObject
        {
            List<T> children = new List<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var o = VisualTreeHelper.GetChild(obj, i);
                if (o != null)
                {
                    if (o is T)
                        children.Add((T)o);

                    children.AddRange(FindVisualChildren<T>(o)); // recursive
                }
            }
            return children;
        }

        public IAnimation? FindLogicalAnimationChild(DependencyObject parent, List<IAnimation> animationControls)
        {
            if (parent == null) return null;

            foreach (var child in LogicalTreeHelper.GetChildren(parent))
            {
                if (child is IAnimation typedChild)
                    animationControls.Add(typedChild);

                if (child is DependencyObject depChild)
                {
                    var result = FindLogicalAnimationChild(depChild, animationControls);
                    if (result != null && result is IAnimation animation)
                        animationControls.Add(animation);
                }
            }
            return null;
        }

        public static T FindUpVisualTree<T>(DependencyObject initial) where T : DependencyObject
        {
            DependencyObject current = initial;

            while (current != null && current.GetType() != typeof(T))
            {
                current = VisualTreeHelper.GetParent(current);
            }
            return current as T;
        }

        public static void FindAllAnimationTextBlock(DependencyObject parent , List<IAnimation> animationControls)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is IAnimation)
                {
                    var animatinoControl = (IAnimation)child;

                    animationControls.Add(animatinoControl);
                }

                // 자식이 또 다른 컨트롤을 가지고 있을 수 있으므로 재귀적으로 호출
                FindAllAnimationTextBlock(child, animationControls);
            }
        }
    }
}
