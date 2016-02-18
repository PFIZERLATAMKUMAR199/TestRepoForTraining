using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.Specialized;

namespace SEMP.Controls
{
    public partial class ExceptionListPresenter : UserControl, INotifyPropertyChanged
    {
        public ExceptionListPresenter()
        {
            DataContext = this;
            _Exceptions.CollectionChanged += Exceptions_CollectionChanged;
            InitializeComponent();
        }
        private void Exceptions_CollectionChanged(Object Sender, NotifyCollectionChangedEventArgs Args)
        {
            RaisePropertyChanged("Exceptions");
        }
        private ObservableCollection<Exception> _Exceptions = new ObservableCollection<Exception>();
        public ObservableCollection<Exception> Exceptions
        {
            get { return _Exceptions; }
            set
            {
                _Exceptions = value;
                _Exceptions.CollectionChanged += Exceptions_CollectionChanged;
                RaisePropertyChanged("Exceptions");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if ((handler != null)) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private void btnDelete_Click(Object Sender, RoutedEventArgs Args)
        {
            Exceptions.Remove((Exception)((FrameworkElement)Sender).DataContext);
        }
    }
}
