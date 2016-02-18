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
{//luis - 2011-10-17

    public partial class RelativeActiveOfUser
    {//luis - 2011-11-02
        private static int _nUser;
        public static int nUser
        {
            get { return _nUser; }
            set
            {
                int nUserOld = _nUser;
                _nUser = value;
                if (_nUser != 0 && nUserOld != _nUser) ORM.Current.RelativeGetActiveOfUserAllAsync(_nUser);
            }
        }
        private static Boolean HaveChanged = true;
        public static void Load()
        {
            if (HaveChanged)
            {
                HaveChanged = false;
                //ORM.Current.RelativeGetActiveOfUserAllAsync(null);
            }
        }
    }
}
