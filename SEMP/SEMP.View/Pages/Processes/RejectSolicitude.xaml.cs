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
{// FRL - 2011-11-11
    public partial class RejectSolicitudePage : UserControl, IPage
    {
        public RejectSolicitudePage()
            : base()
        {
            ORM.Current.LanguageGetWordsCompleted += Language_GetWordsCompleted;
            InitializeComponent();
            ORM.Current.SolicitudeGetRejectableCompleted += Solicitude_ShowInGrid;
            ORM.Current.RejectSolicitudeRejectCompleted += RejectSolicitude_Completed;
        }
        public void Load(CommandsMenu cmdMnu) { cmdMnu.SaveAction = Save_Click; cmdMnu.FindAction = Find_Click; }
        public void Clear()
        {
            grdToProcess.SelectedIndex = -1; // FRL - 2012-01-05 : This fixes bug when closing session with Datagrid in Edit Mode
            // <<-- FRL 2011-11-14
            var DisSolicitude = new RejectSolicitude();
            DataContext = DisSolicitude;
            // -->>
        }
        private void Language_GetWordsCompleted(Object Sender, LanguageGetWordsCompletedEventArgs Args)
        {
            grdToProcess.Columns[1].Header = App.Lang["Solicitude"];
            grdToProcess.Columns[2].Header = App.Lang["Product"];
            grdToProcess.Columns[3].Header = App.Lang["Ammount"];
            grdToProcess.Columns[4].Header = App.Lang["Dose"];
            grdToProcess.Columns[5].Header = App.Lang["Hours"];
            grdToProcess.Columns[6].Header = App.Lang["Days"];
            grdToProcess.Columns[7].Header = App.Lang["Reject"];
        }

        private void Solicitude_ShowInGrid(Object Sender, SolicitudeGetRejectableCompletedEventArgs Args)
        {
            ((RejectSolicitude)DataContext).PendingSolicitudes = Args.Result;
            ((RejectSolicitude)DataContext).ShowAsChanged();
        }

        private void RejectSolicitude_Completed(Object Sender, RejectSolicitudeRejectCompletedEventArgs Args)
        {
            var pendings = new ObservableCollection<PendingSolicitude>();
            if (Args.Result != null)
            {
                // <<-- FRL 2011-11-14
                //              this.Clear();
                chkSelectsAll.IsChecked = false;
                Find_Click();
                // -->>
            }
        }


        private void Save_Click()
        {
            if (cmbNullificationReason.SelectedIndex > 0)
            {
                // <<-- FRL 2011-11-14
                grdToProcess.SelectedIndex = -1; //THIS LINE MAKES SURE THE DATAGRID UPDATES THE LAST CHANGED ROW. (Datagrids don't update edited data until the editing row is deselected)
                // -->>
                ORM.Current.RejectSolicitudeRejectAsync(((RejectSolicitude)DataContext).PendingSolicitudes, Convert.ToInt32(cmbNullificationReason.SelectedValue), txtNullificationObs.Text);
            }
            else
            {
                throw new Exception("Debe seleccionar una Razón de Anulación");
            }
        }

        private void chkSelectsAll_Changed(object sender, RoutedEventArgs e)
        {
            foreach (var item in ((RejectSolicitude)DataContext).PendingSolicitudes)
            {
                item.Process = Convert.ToBoolean(chkSelectsAll.IsChecked);
            }
        }

        //2011-11-14
        private void Find_Click()
        {
            ((RejectSolicitude)DataContext).dtBegin = dpDateBegin.SelectedDate;
            ((RejectSolicitude)DataContext).dtEnd = dpDateEnd.SelectedDate;
            //          ((RejectSolicitude)DataContext). = cmbSolicitudeStatus.SelectedValue;
            if (((RejectSolicitude)DataContext).dtBegin == null)
            {
                throw new Exception("Debe seleccionar una fecha de inicio");
            }
            else
            {
                ORM.Current.SolicitudeGetRejectableAsync(((RejectSolicitude)DataContext).dtBegin, ((RejectSolicitude)DataContext).dtEnd, ((RejectSolicitude)DataContext).SolicitorUserID);
            }
        }

        // FRL - 2011-12-06
        private void grdChildren_CurrentCellChanged(object sender, EventArgs e)
        {
            grdToProcess.BeginEdit();
        }

    }
}
