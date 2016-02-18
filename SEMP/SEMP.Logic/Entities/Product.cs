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

    public partial class Product : IPageData, ICloneable
    {//luis - 2011-11-02
        static Product()
        {
            ORM.Current.ProductSaveCompleted += Current_ChangeCompleted;
            ORM.Current.ProductDeleteCompleted += Current_ChangeCompleted;
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
                ORM.Current.ProductGetAllAsync();
            }
        }
        public Product() {}
        public void Find() { ORM.Current.ProductGetAsync(this); }
        public void Save() { ORM.Current.ProductSaveAsync(this); }
        public void Delete() { ORM.Current.ProductDeleteAsync(this); }
        public Object Clone() { return new Product(); }

    }
}
