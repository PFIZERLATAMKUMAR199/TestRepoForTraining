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
{//luis - 2011-12-01
    public partial class UserActive 
    {
        private static Boolean HaveChanged = true;
        public static void Load()
        {
        // FRL - 2012-01-06 : Removed HaveChanged check to solve logout-login issue
            //if (HaveChanged)
            //{
                HaveChanged = false;
                ORM.Current.UserActiveGetAllAsync();
            //}
        }
    }
}
