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
    public partial class ApproveSolicitudePage : UserControl, IPage
    {
        CommandsMenu commands;
        public ApproveSolicitudePage()
            : base()
        {
            ORM.Current.LanguageGetWordsCompleted += Language_GetWordsCompleted;
            InitializeComponent();
            // <<-- FRL - 2011-12-06
            //ORM.Current.SolicitudeGetCompleted += Solicitude_ShowInGrid;
            ORM.Current.SolicitudeGetToApproveCompleted += Solicitude_ShowInGrid;
            // -->>
            ORM.Current.ApproveSolicitudeApproveCompleted += ApproveSolicitude_Completed;
            ORM.Current.SolicitudeGetItemsCompleted += SolicitudeGetItems_Completed;
        }
        public void Load(CommandsMenu cmdMnu)
        {
            commands = cmdMnu;
            cmdMnu.SaveAction = Save_Click;
            cmdMnu.ReloadAction = Clear; // FRL - 2012-01-09
            //<<-- FRL - 2012-07-02
            grdToApprove.Visibility = Visibility.Visible;
            grdSolicitudeDetail.Visibility = Visibility.Collapsed;
            ((ApproveSolicitude)DataContext).PendingSolicitudeDetail = null;
            txbSelectsAll.Visibility = Visibility.Visible;
            chkSelectsAll.Visibility = Visibility.Visible;
            //-->>
        }
        public void Clear()
        {
            grdToApprove.SelectedIndex = -1; // FRL - 2012-01-05 : This fixes bug when closing session with Datagrid in Edit Mode
            var ProcSolicitude = new ApproveSolicitude();
            DataContext = ProcSolicitude;
            ORM.Current.SolicitudeGetToApproveAsync();
        }
        private void Language_GetWordsCompleted(Object Sender, LanguageGetWordsCompletedEventArgs Args)
        {
            grdToApprove.Columns[0].Header = App.Lang["Solicitude"];
            grdToApprove.Columns[1].Header = App.Lang["Solicitor"];
            grdToApprove.Columns[2].Header = App.Lang["Relative"];
            grdToApprove.Columns[3].Header = App.Lang["Kinship"];
            grdToApprove.Columns[4].Header = App.Lang["ReceptionDate"];
            grdToApprove.Columns[5].Header = "Receta Inicio Fecha";
            grdToApprove.Columns[6].Header = "Receta Fin Fecha";
            grdToApprove.Columns[7].Header = "Tratamiento";
            grdToApprove.Columns[8].Header = App.Lang["Approve"];

            grdSolicitudeDetail.Columns[0].Header = App.Lang["Product"];
            grdSolicitudeDetail.Columns[1].Header = App.Lang["Dose"];
            grdSolicitudeDetail.Columns[2].Header = App.Lang["Hours"];
            grdSolicitudeDetail.Columns[3].Header = App.Lang["Ammount"];
            //neha 17-oct-2013
            grdSolicitudeDetail.Columns[4].Header = "Tratamiento";
            grdSolicitudeDetail.Columns[5].Header = "Fecha Expiracion";
            grdSolicitudeDetail.Columns[6].Header = "Receta Inicio Fecha";
            grdSolicitudeDetail.Columns[7].Header = "Receta Fin Fecha";
        }

        private void Solicitude_ShowInGrid(Object Sender, SolicitudeGetToApproveCompletedEventArgs Args)
        {
            var pendings = new ObservableCollection<PendingSolicitude>();
            Args.Result.AsEnumerable().ToList().ForEach(x => pendings.Add(new PendingSolicitude() { ID = x.ID, Name = x.Name, StatusID = x.StatusID, ReceptionDate = x.ReceptionDate, RelativeName = x.RelativeName, KinshipName = x.KinshipName,dtReceiptFinalDate=x.dtReceiptFinalDate,dtReceiptInitialDate=x.dtReceiptInitialDate,sSolicitudeType=x.sSolicitudeType,Process = false }));
            ((ApproveSolicitude)DataContext).PendingSolicitudes = pendings;
            ((ApproveSolicitude)DataContext).ShowAsChanged();
        }

        private void ApproveSolicitude_Completed(Object Sender, ApproveSolicitudeApproveCompletedEventArgs Args)
        {
            var pendings = new ObservableCollection<PendingSolicitude>();
            if (Args.Result.Value != 0)
            {
                this.Clear();
            }
        }


        /*    public override void ConfigColumn(DataGridAutoGeneratingColumnEventArgs Args)
            {
                var col = Args.Column;
                var bind = ((DataGridBoundColumn)col).Binding;
                switch (col.Header.ToString())
                {
                    case "ID":
                    case "Item":
                    case "Detail":
                    case "Children":
                        Args.Cancel = true;
                        break;
                }
            }
      */
        private void Save_Click()
        {
            // <<-- FRL 2011-11-14
            grdToApprove.SelectedIndex = -1; //THIS LINE MAKES SURE THE DATAGRID UPDATES THE LAST CHANGED ROW. (Datagrids don't update source until the editing row is deselected)
            // -->>
            ORM.Current.ApproveSolicitudeApproveAsync(((ApproveSolicitude)DataContext).PendingSolicitudes);
        }

        private void chkSelectsAll_Changed(object sender, RoutedEventArgs e)
        {
            foreach (var item in ((ApproveSolicitude)DataContext).PendingSolicitudes)
            {
                item.Process = Convert.ToBoolean(chkSelectsAll.IsChecked);
            }
        }

        // FRL - 2011-12-06
        private void grdChildren_CurrentCellChanged(object sender, EventArgs e)
        {
            grdToApprove.BeginEdit();
        }

        // <<-- FRL - 2012-06-22
        private void grdToApprove_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //commands.CancelAction = Back_Action; 
            ORM.Current.SolicitudeGetItemsAsync(new Solicitude() { ID = ((Solicitude)grdToApprove.SelectedItem).ID });
            //grdToApprove.Visibility = Visibility.Collapsed;
            //grdSolicitudeDetail.Visibility = Visibility.Visible;
           // ((ApproveSolicitude)DataContext).PendingSolicitudeDetail = ((PendingSolicitude)grdToApprove.SelectedItem).Children;
         
        }

        private void Back_Action()
        {
            commands.CancelAction = CommandsMenu.NoneAction;
            Load(commands);
            grdToApprove.Visibility = Visibility.Visible;
            grdSolicitudeDetail.Visibility = Visibility.Collapsed;
            ((ApproveSolicitude)DataContext).PendingSolicitudeDetail = null;
            txbSelectsAll.Visibility = Visibility.Visible;
            chkSelectsAll.Visibility = Visibility.Visible;
        }
        private void SolicitudeGetItems_Completed(Object Sender, SolicitudeGetItemsCompletedEventArgs Args)
        {
            // <<-- FRL 2012-06-26
            var nuevo = new ObservableCollection<SolicitudeItem>();
            foreach (var x in Args.Result) { nuevo.Add(x); }
            ((ApproveSolicitude)DataContext).PendingSolicitudeDetail = nuevo;
            grdSolicitudeDetail.UpdateBindingSource(); //Fixes RisePropertyChanged Error. Dunno why the error happens, dunno why this fixes it.
            // -->>
            ((ApproveSolicitude)DataContext).ShowAsChanged("PendingSolicitudeDetail");
            txbSelectsAll.Visibility = Visibility.Collapsed;
            chkSelectsAll.Visibility = Visibility.Collapsed;
            commands.CancelAction = Back_Action;
            commands.ReloadAction = CommandsMenu.NoneAction;
            commands.SaveAction = CommandsMenu.NoneAction;
            grdToApprove.Visibility = Visibility.Collapsed;
            grdSolicitudeDetail.Visibility = Visibility.Visible;
        }
        // -->>
    }
}
