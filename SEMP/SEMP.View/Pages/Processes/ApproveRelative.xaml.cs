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
{// FRL - 2012-06-15
    public partial class ApproveRelativePage : UserControl, IPage
    {
        public ApproveRelativePage()
            : base()
        {
            ORM.Current.LanguageGetWordsCompleted += Language_GetWordsCompleted;
            InitializeComponent();
            ORM.Current.RelativeGetPendingCompleted += Relative_ShowInGrid;
            ORM.Current.ApproveRelativeProcessCompleted += ApproveRelative_Completed;
        }
        public void Load(CommandsMenu cmdMnu)
        {
            cmdMnu.SaveAction = Save_Click;
            cmdMnu.ReloadAction = Clear; // FRL - 2012-01-09
        }
        public void Clear()
        {
            grdToProcess.SelectedIndex = -1; // FRL - 2012-01-05 : This fixes bug when closing session with Datagrid in Edit Mode
            var ProcRelative = new ApproveRelative();
            DataContext = ProcRelative;
            ORM.Current.RelativeGetPendingAsync();
            // -->>
        }
        private void Language_GetWordsCompleted(Object Sender, LanguageGetWordsCompletedEventArgs Args)
        {
            grdToProcess.Columns[1].Header = App.Lang["Relative"];
            grdToProcess.Columns[2].Header = App.Lang["Kinship"];
            grdToProcess.Columns[3].Header = App.Lang["KinshipKind"];
            grdToProcess.Columns[4].Header = App.Lang["User"];
            grdToProcess.Columns[5].Header = App.Lang["Process"];
        }

        private void Relative_ShowInGrid(Object Sender, RelativeGetPendingCompletedEventArgs Args)
        {
            //var pendings = new ObservableCollection<Relative>();
            //Args.Result.AsEnumerable().ToList().ForEach(x => pendings.Add(new Relative() { ID = x.ID, Name = x.Name, StatusID = x.StatusID, ReceptionDate = x.ReceptionDate, Process = false }));
            ((ApproveRelative)DataContext).PendingRelatives = Args.Result;
            ((ApproveRelative)DataContext).ShowAsChanged();
        }

        private void ApproveRelative_Completed(Object Sender, ApproveRelativeProcessCompletedEventArgs Args)
        {
            var pendings = new ObservableCollection<Relative>();
            //        if (Args.Result.Value != null)
            //      {
            this.Clear();
            //    }
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
            grdToProcess.SelectedIndex = -1; //THIS LINE MAKES SURE THE DATAGRID UPDATES THE LAST CHANGED ROW. (Datagrids don't update source until the editing row is deselected)
            // -->>
            ORM.Current.ApproveRelativeProcessAsync(((ApproveRelative)DataContext).PendingRelatives);
        }

        private void chkSelectsAll_Changed(object sender, RoutedEventArgs e)
        {
            foreach (var item in ((ApproveRelative)DataContext).PendingRelatives)
            {
                item.Process = Convert.ToBoolean(chkSelectsAll.IsChecked);
            }

        }

        // FRL - 2011-12-06
        private void grdChildren_CurrentCellChanged(object sender, EventArgs e)
        {
            grdToProcess.BeginEdit();
        }

    }
}
