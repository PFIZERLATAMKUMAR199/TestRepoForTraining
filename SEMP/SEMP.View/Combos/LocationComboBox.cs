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
{// luis - 2012-07-05
    public class LocationComboBox : NamedEntityComboBox
    {
        private static NamedEntityHelper Helper = new NamedEntityHelper();
        static LocationComboBox() { ORM.Current.LocationGetAllCompleted += Helper.WCF_SetDefault; }
        private static void This_Loaded(Object Sender, EventArgs Args) { Location.Load(); }
        public LocationComboBox()
        {
            Load(Helper.Source);
            ORM.Current.LocationGetAllCompleted += WCF_GetAll;
            this.Loaded += This_Loaded;
        }
    }
}
