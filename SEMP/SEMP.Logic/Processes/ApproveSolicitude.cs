using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SEMP.Controls;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;

namespace SEMP.Logic.WCF.Services
{//FRL - 2011-11-01
    public partial class ApproveSolicitude : NamedEntity
    {
        public ObservableCollection<PendingSolicitude> PendingSolicitudes { get; set; }
        public ObservableCollection<SolicitudeItem> PendingSolicitudeDetail { get; set; }
        static ApproveSolicitude()
        {
            ORM.Current.ApproveSolicitudeApproveCompleted += Current_ChangeCompleted;
        }
        public void ShowAsChanged()
        {
            RaisePropertyChanged("PendingSolicitudes");
        }
        public void ShowAsChanged(string proper)
        {
            RaisePropertyChanged(proper);
        }
        private static Boolean HaveChanged = true;
        private static void Current_ChangeCompleted(Object Sender, AsyncCompletedEventArgs Args)
        {
            if (Args.Error == null) HaveChanged = true;
        }
        public static void Load()
        {
            if (HaveChanged)
            {
                HaveChanged = false;
            }
        }

    }
}
