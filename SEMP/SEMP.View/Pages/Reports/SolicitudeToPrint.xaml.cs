using System;
using System.Collections;
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
using SEMP.Controls;
using SEMP.Logic.WCF.Services;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SEMP.View.Pages
{
    public partial class SolicitudeToPrintPage : UserControl, IPage
    {//luis - 2011-11-09
        private IList _Source = new ObservableCollection<String>();
        public SolicitudeToPrintPage()
            : base()
        {
            InitializeComponent();
            ORM.Current.SolicitudeToPrintReportCompleted += WCF_SolicitudeToPrintReportCompleted;
            dgReport.ItemsSource = _Source;
            Clear();
        }
        private void WCF_SolicitudeToPrintReportCompleted(Object Sender, SolicitudeToPrintReportCompletedEventArgs Args)
        {
            if (Args.Error != null) throw Args.Error;
            hlbReport.Content = Args.Result;
            hlbReport.NavigateUri = new Uri(Args.Result);
            _Source.Add(Args.Result);
        }
        public void Load(CommandsMenu cmdMnu) { cmdMnu.ReportAction = Report_Click; }
        public void Clear()
        {
            var Report = new SolicitudeToPrint();
            DataContext = Report;
            hlbReport.Content = " ";
            _Source.Clear();
        }
        private void Report_Click()
        {
            hlbReport.Content = " ";
            if (dpDateBegin.SelectedDate == null)
                ((SolicitudeToPrint)DataContext).dtBegin = null;
            else
                ((SolicitudeToPrint)DataContext).dtBegin = dpDateBegin.SelectedDate.Value;
            if (dpDateEnd.SelectedDate == null)
                ((SolicitudeToPrint)DataContext).dtEnd = null;
            else
                ((SolicitudeToPrint)DataContext).dtEnd = dpDateEnd.SelectedDate.Value;
            
            ((SolicitudeToPrint)DataContext).Report();
        }
    }
}
