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
{//luis - 2012-06-18
    public partial class SalesAdministrationReportPage : UserControl, IPage
    {
        private IList _Source = new ObservableCollection<String>();
        public SalesAdministrationReportPage()
            : base()
        {
            InitializeComponent();
            ORM.Current.SalesAdministrationReportCompleted += WCF_SalesAdministrationReportCompleted;
            dgReport.ItemsSource = _Source;
            Clear();
        }
        private void WCF_SalesAdministrationReportCompleted(Object Sender, SalesAdministrationReportCompletedEventArgs Args)
        {
            if (Args.Error != null) throw Args.Error;
            hlbReport.Content = Args.Result;
            hlbReport.NavigateUri = new Uri(Args.Result);
            _Source.Add(Args.Result);
        }
        public void Load(CommandsMenu cmdMnu) { cmdMnu.ReportAction = Report_Click; }
        public void Clear()
        {
            var Report = new SalesAdministrationReport();
            DataContext = Report;
            hlbReport.Content = " ";
            _Source.Clear();
        }
        private void Report_Click()
        {
            hlbReport.Content = " ";
            InputValidation();
            ((SalesAdministrationReport)DataContext).dtBegin = Convert.ToDateTime(dpDateBegin.Text);
            ((SalesAdministrationReport)DataContext).dtEnd = Convert.ToDateTime(dpDateEnd.Text);
            ((SalesAdministrationReport)DataContext).Report();
        }

        private void InputValidation()
        {
            if (dpDateBegin.Text == "" || dpDateEnd.Text == "")
            {
                throw new Exception("Debe ingresar ambas fechas");
            }
            else if (Convert.ToDateTime(dpDateBegin.Text) > Convert.ToDateTime(dpDateEnd.Text))
            {
                throw new Exception("La fecha desde debe ser menor a la fecha hasta");
            }
        }
    }
}
