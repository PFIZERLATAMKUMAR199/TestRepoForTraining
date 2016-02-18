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
using System.ComponentModel;
using System.Windows.Data;

namespace SEMP.Controls
{
    public partial class ExceptionPresenter : UserControl
    {
        public ExceptionPresenter()
        {
            InitializeComponent();
            MainExpander.SetBinding(Expander.DataContextProperty, new Binding("Exception") { Source = this });
        }
        #region ExceptionDependencyProperty
        public static readonly DependencyProperty ExceptionProperty =
            DependencyProperty.Register("Exception", typeof(Exception), typeof(ExceptionPresenter),
            new PropertyMetadata(new PropertyChangedCallback(ExceptionPropertyChanged)));
        private static void ExceptionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue) ((ExceptionPresenter)d).Exception = (Exception)e.NewValue;
        }
        public Exception Exception
        {
            get { return (Exception)GetValue(ExceptionPresenter.ExceptionProperty); }
            set { SetValue(ExceptionPresenter.ExceptionProperty, value); }
        }
        #endregion ExceptionDependencyProperty
        private void btnDelete_Click(Object Sender, RoutedEventArgs Args)
        {
            ((FrameworkElement)MainExpander.FindName("expInnerException")).Visibility = Visibility.Collapsed;
        }
    }
}
