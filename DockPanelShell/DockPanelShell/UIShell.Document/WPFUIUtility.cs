using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UIShell.Document
{
    internal partial class UIUtility
    {
        public static object FindWpfFocusedControl(DependencyObject root)
        {
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                DependencyObject current = queue.Dequeue();
                var control = current as Control;
                if (control != null && control.IsTabStop && control.IsFocused)
                {
                    return control;
                }
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(current, i);
                    if (child != null)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
            return null;
        }
    }
}