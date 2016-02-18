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

namespace SEMP.View.Pages
{// FRL - 2012-06-18
    public partial class ProcessSolicitudePage : UserControl, IPage
    {
        CommandsMenu commands;
        public ProcessSolicitudePage()
            : base()
        {
            ORM.Current.LanguageGetWordsCompleted += Language_GetWordsCompleted;
            InitializeComponent();
            // <<-- FRL - 2011-12-06
            //ORM.Current.SolicitudeGetCompleted += Solicitude_ShowInGrid;
            ORM.Current.SolicitudeGetToProcessCompleted += Solicitude_ShowInGrid;
            // -->>
            ORM.Current.ProcessSolicitudeProcessCompleted += ProcessSolicitude_Completed;
        }
        public void Load(CommandsMenu cmdMnu)
        {
            commands = cmdMnu;
            cmdMnu.SaveAction = Save_Click;
            cmdMnu.ReloadAction = Clear; // FRL - 2012-01-09
        }
        public void Clear()
        {
            grdToProcess.SelectedIndex = -1; // FRL - 2012-01-05 : This fixes bug when closing session with Datagrid in Edit Mode
            var ProcSolicitude = new ProcessSolicitude();
            DataContext = ProcSolicitude;
            ORM.Current.SolicitudeGetToProcessAsync();
        }
        private void Language_GetWordsCompleted(Object Sender, LanguageGetWordsCompletedEventArgs Args)
        {
            grdToProcess.Columns[0].Header = App.Lang["Solicitude"];
            grdToProcess.Columns[1].Header = App.Lang["Solicitor"];
            grdToProcess.Columns[2].Header = App.Lang["ReceptionDate"];
            grdToProcess.Columns[3].Header = "Fecha Aprobacion";
            grdToProcess.Columns[4].Header = App.Lang["Process"];
        }

        private void Solicitude_ShowInGrid(Object Sender, SolicitudeGetToProcessCompletedEventArgs Args)
        {
            var pendings = new ObservableCollection<PendingSolicitude>();
            Args.Result.AsEnumerable().ToList().ForEach(x => pendings.Add(new PendingSolicitude() { ID = x.ID, Name = x.Name, StatusID = x.StatusID, ReceptionDate = x.ReceptionDate,ApprovalDate=x.ApprovalDate, Process = false }));
            ((ProcessSolicitude)DataContext).PendingSolicitudes = pendings;
            ((ProcessSolicitude)DataContext).ShowAsChanged();
        }

        private void ProcessSolicitude_Completed(Object Sender, ProcessSolicitudeProcessCompletedEventArgs Args)
        {
            var pendings = new ObservableCollection<PendingSolicitude>();
            if (Args.Result.Value != 0)
            {
                this.Clear();
            }
        }


        //public override void ConfigColumn(DataGridAutoGeneratingColumnEventArgs Args)
        //{
        //    var col = Args.Column;
        //    var bind = ((DataGridBoundColumn)col).Binding;
        //    switch (col.Header.ToString())
        //    {
        //        case "ID":
        //        case "Item":
        //        case "Detail":
        //        case "Children":
        //            Args.Cancel = true;
        //            break;
        //    }
        //}
      
        private void Save_Click()
        {
            // <<-- FRL 2011-11-14
            grdToProcess.SelectedIndex = -1; //THIS LINE MAKES SURE THE DATAGRID UPDATES THE LAST CHANGED ROW. (Datagrids don't update source until the editing row is deselected)
            // -->>
            ORM.Current.ProcessSolicitudeProcessAsync(((ProcessSolicitude)DataContext).PendingSolicitudes);
        }

        private void chkSelectsAll_Changed(object sender, RoutedEventArgs e)
        {
            foreach (var item in ((ProcessSolicitude)DataContext).PendingSolicitudes)
            {
                item.Process = Convert.ToBoolean(chkSelectsAll.IsChecked);
            }
        }

        // FRL - 2011-12-06
        private void grdChildren_CurrentCellChanged(object sender, EventArgs e)
        {
            grdToProcess.BeginEdit();
        }

        // FRL - 2012-06-22
        private void grdToProcess_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            commands.CancelAction = Back_Action;
            ((ProcessSolicitude)DataContext).PendingSolicitudeDetail = ((PendingSolicitude)grdToProcess.SelectedItem).Children;
        }

        private void Back_Action()
        {
            commands.CancelAction = CommandsMenu.NoneAction;
            ((ProcessSolicitude)DataContext).PendingSolicitudeDetail = null;
        }
    }
}
