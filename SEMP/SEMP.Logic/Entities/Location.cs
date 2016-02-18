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
{//luis - 2012-07-05
    public partial class Location : IPageData, ICloneable
    {
        static Location()
        {
            ORM.Current.LocationSaveCompleted += Current_ChangeCompleted;
            ORM.Current.LocationDeleteCompleted += Current_ChangeCompleted;
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
                ORM.Current.LocationGetAllAsync();
            }
        }
        public void Find() { ORM.Current.LocationGetAsync(this); }
        public void Save() { ORM.Current.LocationSaveAsync(this); }
        public void Delete() { ORM.Current.LocationDeleteAsync(this); }
        public Object Clone() { return new Location(); }
    }
}
