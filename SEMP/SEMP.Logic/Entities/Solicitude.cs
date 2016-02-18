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
using System.Collections;               //IList
using System.Collections.ObjectModel;   //ObservableCollection

namespace SEMP.Logic.WCF.Services
{//luis - 2012-06-12
    public partial class Solicitude : NamedEntity, IPageDataParent
    {
 
        private ObservableCollection<SolicitudeProductsOfUserResult> _UserSolicitudes;
        public ObservableCollection<SolicitudeProductsOfUserResult> UserSolicitudes
        {
            get { return _UserSolicitudes; }
            set
            {
                _UserSolicitudes = value;
                RaisePropertyChanged("UserSolicitudes");
            }
        } 

        static Solicitude()
        {
            ORM.Current.SolicitudeSaveCompleted += Current_ChangeCompleted;
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

        public Solicitude()
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
