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
{//luis - 2011-10-24
    public partial class User : IPageData, ICloneable
    {//FRL - 2011-10-24
        private static Boolean HaveChanged = true;
        public static void Load()
        {
            if (HaveChanged)
            {
                HaveChanged = false;
                ORM.Current.UserGetAllAsync();
            }
        }
        public void Find() { ORM.Current.UserGetAsync(this); }
        public void Save() { ORM.Current.UserSaveAsync(this); }
        public void Delete() { ORM.Current.UserDeleteAsync(this); }
        public object Clone() { return null; }

    }
}
