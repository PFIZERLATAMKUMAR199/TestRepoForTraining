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
    public partial class PopupControl : UserControl
    {
        public PopupControl()
        {
            InitializeComponent();
            
            btnClosePopup.Click += new System.Windows.RoutedEventHandler(btnClosePopup_Click); 
            
        }

        private void btnClosePopup_Click(object sender, System.Windows.RoutedEventArgs e)
        { this.Close(); }

        public void Close() 
        { 
            popMessage.IsOpen = false;
            this.Visibility = System.Windows.Visibility.Collapsed;
            
        }
        public void Show()
        {
            
            popMessage.IsOpen = true; 
            this.Visibility = System.Windows.Visibility.Visible;
            btnClosePopup.Focus();
        }
    }
}
