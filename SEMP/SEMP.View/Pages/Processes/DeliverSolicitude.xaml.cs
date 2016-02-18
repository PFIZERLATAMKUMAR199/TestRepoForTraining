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
{// FRL - 2011-11-14
    public partial class DeliverSolicitudePage : UserControl, IPage
    {
        public DeliverSolicitudePage()
            : base()
        {
            ORM.Current.LanguageGetWordsCompleted += Language_GetWordsCompleted;
            InitializeComponent();
            // <<-- FRL - 2011-12-06
            //ORM.Current.SolicitudeGetCompleted += Solicitude_ShowInGrid;
            ORM.Current.SolicitudeGetToDeliverCompleted += Solicitude_ShowInGrid;
            // -->>
            ORM.Current.DeliverSolicitudeDeliverCompleted += DeliverSolicitude_Completed;
        }
        public void Load(CommandsMenu cmdMnu)
        {
            cmdMnu.SaveAction = Save_Click;
            cmdMnu.ReloadAction = Clear; // FRL - 2012-01-09
        }
        public void Clear()
        {
            grdToDeliver.SelectedIndex = -1; // FRL - 2012-01-05 : This fixes bug when closing session with Datagrid in Edit Mode
            var ProcSolicitude = new DeliverSolicitude();
            DataContext = ProcSolicitude;
            // <<-- FRL 2011-12-06
            //ORM.Current.SolicitudeGetAsync(new Solicitude() { StatusID = 2 }); //constant 2 stands for the id of SolicitudeStatus "Ready to Deliver"
            ORM.Current.SolicitudeGetToDeliverAsync();
            // -->>
        }
        private void Language_GetWordsCompleted(Object Sender, LanguageGetWordsCompletedEventArgs Args)
        {
            grdToDeliver.Columns[1].Header = App.Lang["Code"];
            grdToDeliver.Columns[2].Header = App.Lang["Solicitude"];
            grdToDeliver.Columns[3].Header = App.Lang["ReceptionDate"];
            //Neha 22-10-2013
            grdToDeliver.Columns[4].Header = "Fecha Aprobacion";
            grdToDeliver.Columns[5].Header = "Fecha Armado";
            //Functionality removed grdToDeliver.Columns[6].Header = App.Lang["DispatchDate"];
            grdToDeliver.Columns[6].Header = App.Lang["Deliver"];
        }

        private void Solicitude_ShowInGrid(Object Sender, SolicitudeGetToDeliverCompletedEventArgs Args)
        {
            var pendings = new ObservableCollection<PendingSolicitude>();
            //Neha 22-10-2013
            Args.Result.AsEnumerable().ToList().ForEach(x => pendings.Add(new PendingSolicitude() { ID = x.ID, Name = x.Name, StatusID = x.StatusID, ReceptionDate = x.ReceptionDate, ApprovalDate=x.ApprovalDate, PreparationDate=x.PreparationDate, Process = false }));
            ((DeliverSolicitude)DataContext).PendingSolicitudes = pendings;
            ((DeliverSolicitude)DataContext).ShowAsChanged();
        }

        private void DeliverSolicitude_Completed(Object Sender, DeliverSolicitudeDeliverCompletedEventArgs Args)
        {
            var pendings = new ObservableCollection<PendingSolicitude>();
            if (Args.Result.Value != null)
            {
                this.Clear();
            }
        }


        private void Save_Click()
        {
            grdToDeliver.SelectedIndex = -1; //THIS LINE MAKES SURE THE DATAGRID UPDATES THE LAST CHANGED ROW. (Datagrids don't update source until the editing row is deselected)
            ORM.Current.DeliverSolicitudeDeliverAsync(((DeliverSolicitude)DataContext).PendingSolicitudes);
        }

        private void chkSelectsAll_Changed(object sender, RoutedEventArgs e)
        {
            foreach (var item in ((DeliverSolicitude)DataContext).PendingSolicitudes)
            {
                item.Process = Convert.ToBoolean(chkSelectsAll.IsChecked);
            }

        }

        // FRL - 2011-12-06
        private void grdChildren_CurrentCellChanged(object sender, EventArgs e)
        {
            grdToDeliver.BeginEdit();
        }

    }
}
