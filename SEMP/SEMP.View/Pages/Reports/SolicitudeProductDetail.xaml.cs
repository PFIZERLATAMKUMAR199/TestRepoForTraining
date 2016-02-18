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
{//luis - 2011-10-12
    public partial class SolicitudeProductDetailPage : UserControl, IPage
    {
        private IList _Source = new ObservableCollection<String>();
        public SolicitudeProductDetailPage()
            : base()
        {
            InitializeComponent();
            ORM.Current.SolicitudeProductDetailReportCompleted += WCF_SolicitudeProductDetailReportCompleted;
            dgReport.ItemsSource = _Source;
            Clear();
        }
        private void WCF_SolicitudeProductDetailReportCompleted(Object Sender, SolicitudeProductDetailReportCompletedEventArgs Args)
        {
            if (Args.Error != null) throw Args.Error;
            hlbReport.Content = Args.Result;
            hlbReport.NavigateUri = new Uri(Args.Result);
            _Source.Add(Args.Result);
        }
        public void Load(CommandsMenu cmdMnu) { cmdMnu.ReportAction = Report_Click; }
        public void Clear()
        {
            var Report = new SolicitudeProductDetail();
            DataContext = Report;
            hlbReport.Content = " ";
            _Source.Clear();
        }
        private void Report_Click()
        {
            hlbReport.Content = " ";
            InputValidation();
            ((SolicitudeProductDetail)DataContext).dtBegin = Convert.ToDateTime(dpDateBegin.Text);
            ((SolicitudeProductDetail)DataContext).dtEnd = Convert.ToDateTime(dpDateEnd.Text);
            ((SolicitudeProductDetail)DataContext).Report();
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
