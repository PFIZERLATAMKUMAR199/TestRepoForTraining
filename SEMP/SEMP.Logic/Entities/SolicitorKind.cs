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
{//luis - 2011-10-12

    // FRL - 2011-11-22
    public partial class SolicitorKind : IPageData, ICloneable, IPageDataParent
    {//luis - 2011-11-02
        static SolicitorKind()
        {
            ORM.Current.SolicitorKindSaveCompleted += Current_ChangeCompleted;
            ORM.Current.SolicitorKindDeleteCompleted += Current_ChangeCompleted;
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
                ORM.Current.SolicitorKindGetAllAsync();
            }
        }
        public void Find() { ORM.Current.SolicitorKindGetAsync(this); }
        public void Save() { ORM.Current.SolicitorKindSaveAsync(this); }
        public void Delete() { ORM.Current.SolicitorKindDeleteAsync(this); }
        public Object Clone() { return new SolicitorKind(); }

        // <<-- FRL - 2011-11-22

        public SolicitorKind()
        {
            Detail = new ObservableCollection<ProductSolicitorKind>();
        }

        public void NewChild(String Children) { Detail.Add(new ProductSolicitorKind()); RaisePropertyChanged("Children"); }

        public IList this[String iChildren] { set { Children = value; } get { return Children; } }
        public IList Children
        {
            set { Detail = (ObservableCollection<ProductSolicitorKind>)value; }
            get
            {
                if (Detail == null && ID != null)
                {
                    ORM.Current.SolicitorKindGetChildrenCompleted += Current_SolicitorKindGetChildrenCompleted;
                    ORM.Current.SolicitorKindGetChildrenAsync(this);
                }
                return Detail;
            }
        }
        private void Current_SolicitorKindGetChildrenCompleted(Object Sender, SolicitorKindGetChildrenCompletedEventArgs Args)
        {
            Detail = Args.Result;
            RaisePropertyChanged("Children");
            ORM.Current.SolicitorKindGetChildrenCompleted -= Current_SolicitorKindGetChildrenCompleted;
        }
        //>>

    }
}
