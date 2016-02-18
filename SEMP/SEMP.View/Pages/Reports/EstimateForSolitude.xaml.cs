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
{//luis - 2012-06-14

    public partial class EstimateForSolitudePage : UserControl, IPage
    {
        private IList _Source = new ObservableCollection<String>();
        public EstimateForSolitudePage()
            : base()
        {
            InitializeComponent();
            ORM.Current.EstimateForSolitudeReportCompleted += WCF_EstimateForSolitudeReportCompleted;
            dgReport.ItemsSource = _Source;
            Clear();
        }
        private void WCF_EstimateForSolitudeReportCompleted(Object Sender, EstimateForSolitudeReportCompletedEventArgs Args)
        {
            if (Args.Error != null) throw Args.Error;
            hlbReport.Content = Args.Result;
            hlbReport.NavigateUri = new Uri(Args.Result);
            _Source.Add(Args.Result);
        }
        public void Load(CommandsMenu cmdMnu) { cmdMnu.ReportAction = Report_Click; }
        public void Clear()
        {
            var Report = new EstimateForSolitude();
            DataContext = Report;
            hlbReport.Content = " ";
            _Source.Clear();
        }
        private void Report_Click()
        {
            hlbReport.Content = " ";
            ((EstimateForSolitude)DataContext).Report();
        }
    }
}
