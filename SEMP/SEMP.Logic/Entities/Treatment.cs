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

namespace SEMP.Logic.WCF.Services
{//luis - 2012-06-21
    public partial class Treatment : IPageData, ICloneable
    {
        static Treatment()
        {
            //ORM.Current.TreatmentSaveCompleted += Current_ChangeCompleted;
            //ORM.Current.TreatmentDeleteCompleted += Current_ChangeCompleted;
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
                ORM.Current.TreatmentGetAllAsync();
            }
        }
        public void Find() { ORM.Current.TreatmentGetAsync(this); }
        public void Save() {}// ORM.Current.TreatmentSaveAsync(this); }
        public void Delete() {}// ORM.Current.TreatmentDeleteAsync(this); }
        public Object Clone() { return new Treatment(); }
    }
}
