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
    public partial class ModifySolicitude :  IPageDataParent
    {
        
        
        
        private ObservableCollection<SolicitudeProductsOfUserModifyResult> _UserSolicitudesModify;
        public ObservableCollection<SolicitudeProductsOfUserModifyResult> UserSolicitudesModify
        {
            get { return _UserSolicitudesModify; }
            set
            {
                _UserSolicitudesModify = value;
                RaisePropertyChanged("UserSolicitudesModify");
            }
        }

        //end of code here
        public ObservableCollection<PendingSolicitude> PendingSolicitudes { get; set; }
        public ObservableCollection<SolicitudeItem> PendingSolicitudeDetail { get; set; }
        static ModifySolicitude()
        {
            

            ORM.Current.SolicitudeModifySaveCompleted += Current_ChangeCompleted;
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

        //syed 24 oct 2013

        public ModifySolicitude()
        {
            Detail = new ObservableCollection<SolicitudeItem>();
            ORM.Current.SolicitudeGetLastItemsCompleted += Current_SolicitudeGetChildrenCompleted;
        }

        public void NewChild(String Children) { }

        public IList this[String iChildren] { set { Children = value; } get { return Children; } }

        public IList Children
        {
            set { Detail = (ObservableCollection<SolicitudeItem>)value; }
            get
            {
                if (Detail == null && ID != null)
                {
                    Detail = new ObservableCollection<SolicitudeItem>();
                }
                return Detail;
            }
        }

        private void Current_SolicitudeGetChildrenCompleted(Object Sender, SolicitudeGetLastItemsCompletedEventArgs Args)
        {
            Detail = Args.Result;
            RaisePropertyChanged("Children");
            ORM.Current.SolicitudeGetLastItemsCompleted -= Current_SolicitudeGetChildrenCompleted;
        }

    }
}
