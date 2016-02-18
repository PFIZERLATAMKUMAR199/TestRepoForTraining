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
{//FRL 2011-10-24
    public partial class Relative : IPageData, ICloneable
    {
        static Relative()
        {
            ORM.Current.RelativeSaveCompleted += Current_ChangeCompleted;
            ORM.Current.RelativeDeleteCompleted += Current_ChangeCompleted;
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
                ORM.Current.RelativeGetAllAsync();
            }
        }
        public void Find() { ORM.Current.RelativeGetAsync(this); }
        public void Save() { ORM.Current.RelativeSaveAsync(this); }
        public void Delete() { ORM.Current.RelativeDeleteAsync(this); }
        public Object Clone() { return new Relative(); }
    }
}
