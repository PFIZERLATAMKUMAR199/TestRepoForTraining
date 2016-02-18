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
using SEMP.Controls;
using SEMP.Logic.WCF.Services;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;

namespace SEMP.View.Pages
{//Luis - 2011-12-19
    public partial class SolicitudeToPrintQueryPage : UserControl, IPage
    {
        private IList _Source = new ObservableCollection<String>();
        public SolicitudeToPrintQueryPage()
            : base()
        {
            ORM.Current.LanguageGetWordsCompleted += Language_GetWordsCompleted;
            InitializeComponent();
            ORM.Current.SolicitudeGetToProcessCompleted += Solicitude_ShowInGrid; 
            ORM.Current.SolicitudeToPrintQueryReportCompleted += WCF_SolicitudeToPrintQueryReportCompleted;
            dgReport.ItemsSource = _Source;
            Clear();
        }
        private void WCF_SolicitudeToPrintQueryReportCompleted(Object Sender, SolicitudeToPrintQueryReportCompletedEventArgs Args)
        {
            if (Args.Error != null) throw Args.Error;
            hlbReport.Content = Args.Result;
            hlbReport.NavigateUri = new Uri(Args.Result);
            _Source.Add(Args.Result);
        }
        public void Load(CommandsMenu cmdMnu) 
        {
            cmdMnu.ReportAction = Report_Click;
            cmdMnu.ReloadAction = Clear; // FRL - 2012-01-09
        }

        public void Clear()
        {
            grdToProcess.SelectedIndex = -1; // FRL - 2012-01-05 : This fixes bug when closing session with Datagrid in Edit Mode
            var Report = new SolicitudeToPrintQuery();
            DataContext = Report;
            hlbReport.Content = " ";
            _Source.Clear();

            ORM.Current.SolicitudeGetToProcessAsync();

        }
        private void Language_GetWordsCompleted(Object Sender, LanguageGetWordsCompletedEventArgs Args)
        {
            grdToProcess.Columns[1].Header = App.Lang["Solicitude"];
            grdToProcess.Columns[2].Header = App.Lang["Solicitor"];
            grdToProcess.Columns[3].Header = App.Lang["ReceptionDate"];
            grdToProcess.Columns[4].Header = App.Lang["Print"];
        }

        private void Solicitude_ShowInGrid(Object Sender, SolicitudeGetToProcessCompletedEventArgs Args)
        {
            var MyPendings = new ObservableCollection<PendingSolicitude>();
            Args.Result.AsEnumerable().ToList().ForEach(
                x => MyPendings.Add(new PendingSolicitude() 
                { 
                    ID = x.ID, 
                    Name = x.Name, 
                    StatusID = x.StatusID, 
                    ReceptionDate = x.ReceptionDate,
                    Process = false }));

            ((SolicitudeToPrintQuery)DataContext).Detail = MyPendings;
        }

        private void Report_Click()
        {
            hlbReport.Content = " ";
            grdToProcess.SelectedIndex = -1; //THIS LINE MAKES SURE THE DATAGRID UPDATES THE LAST CHANGED ROW. (Datagrids don't update source until the editing row is deselected)
            ((SolicitudeToPrintQuery)DataContext).Report();
        }

        private void chkSelectsAll_Changed(object sender, RoutedEventArgs e)
        {
            foreach (var item in ((SolicitudeToPrintQuery)DataContext).Detail)
            {
                item.Process = Convert.ToBoolean(chkSelectsAll.IsChecked);
            }
        }

        private void grdChildren_CurrentCellChanged(object sender, EventArgs e)
        {
            grdToProcess.BeginEdit();
        }

    }
}
