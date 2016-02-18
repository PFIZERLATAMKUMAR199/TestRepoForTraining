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
{// luis - 2012-06-13
    public class ProductOriginComboBox : NamedEntitySComboBox
    {
        private static NamedEntitySHelper Helper = new NamedEntitySHelper();
        static ProductOriginComboBox() { ORM.Current.ProductOriginGetAllCompleted += Helper.WCF_SetDefault; }
        private static void This_Loaded(Object Sender, EventArgs Args) { ProductOrigin.Load(); }
        public ProductOriginComboBox()
        {
            Load(Helper.Source);
            ORM.Current.ProductOriginGetAllCompleted += WCF_GetAll;
            this.Loaded += This_Loaded;
        }
    }
}
