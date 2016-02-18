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
{//FRL - 2012-06-15
    public partial class ApproveRelative : NamedEntity
    {
        public ObservableCollection<ListedRelative> PendingRelatives { get; set; }
        static ApproveRelative()
        {
            ORM.Current.ApproveRelativeProcessCompleted += Current_ChangeCompleted;
        }
        public void ShowAsChanged()
        {
            RaisePropertyChanged("PendingRelatives");
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
