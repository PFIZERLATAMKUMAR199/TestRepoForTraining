using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SEMP.Controls
{
    public partial class SubMenu : ItemsControl
    {
        public SubMenu() { InitializeComponent(); }
        private void Button_Click(Object Sender, RoutedEventArgs Args)
        {
            try
            {
                ((FrameworkElement)Parent).Visibility = Visibility.Collapsed;
                ((IDelegateCommand)((FrameworkElement)Sender).DataContext).Execute();
            }
            catch { }
        }
    }
}
