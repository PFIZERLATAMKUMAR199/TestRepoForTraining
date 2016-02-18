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
using System.Threading;

namespace SEMP.Controls
{
    public partial class ImageButton : Button
    {
        public ImageButton()
        {
            InitializeComponent();
            LayoutUpdated += This_LayoutUpdated;
            _NewClickEnabler = new Timer(ClickEnable, this, Timeout.Infinite, Timeout.Infinite);
        }
        private void This_LayoutUpdated(Object Sender, EventArgs Args)
        {
            if (Visibility == Visibility.Collapsed)
                VisualStateManager.GoToState(this, "Normal", false);
        }
        private Timer _NewClickEnabler;
        private void ClickEnable(Object Sender) { Dispatcher.BeginInvoke(delegate { IsEnabled = true; }); }
        private void Button_Click(Object Sender, RoutedEventArgs Args)
        {
            IsEnabled = false;
            _NewClickEnabler.Change(500, Timeout.Infinite);
        }
    }
}
