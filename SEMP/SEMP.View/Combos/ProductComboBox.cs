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
using SEMP.Logic.WCF.Services;
using System.Collections;

namespace SEMP.View
{// luis - 2011-10-12
    public class ProductComboBox : NamedEntityComboBox
    {
        private static NamedEntityHelper Helper = new NamedEntityHelper();
        static ProductComboBox() { ORM.Current.ProductGetAllCompleted += Helper.WCF_SetDefault; }
        private static void This_Loaded(Object Sender, EventArgs Args) { Product.Load(); }
        public ProductComboBox()
        {
            Load(Helper.Source);
            ORM.Current.ProductGetAllCompleted += WCF_GetAll;
            this.Loaded += This_Loaded;
        }
    }
}
