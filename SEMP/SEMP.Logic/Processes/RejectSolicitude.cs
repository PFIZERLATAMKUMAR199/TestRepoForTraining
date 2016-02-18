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
using System.Collections.ObjectModel;

namespace SEMP.Logic.WCF.Services
{//FRL - 2011-11-11
    public partial class RejectSolicitude : NamedEntity
    {
        public ObservableCollection<SolicitudeItem> PendingSolicitudes { get; set; }
        public DateTime? dtBegin;
        public DateTime? dtEnd;
        public int? StatusID { get; set; }
        public int? SolicitorUserID { get; set; }

        static RejectSolicitude()
        {
            ORM.Current.RejectSolicitudeRejectCompleted += Current_ChangeCompleted;
        }
        public void ShowAsChanged()
        {
            RaisePropertyChanged("PendingSolicitudes");
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
