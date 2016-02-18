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

namespace SEMP.Logic.WCF.Services
{//luis - 2011-11-03
    public partial class ProductOrigin
    {
        private static Boolean HaveChanged = true;
        public static void Load()
        {
            if (HaveChanged)
            {
                HaveChanged = false;
                ORM.Current.ProductOriginGetAllAsync();
            }
        }
    }
}
