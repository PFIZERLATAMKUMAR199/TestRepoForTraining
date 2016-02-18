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

    public partial class ProductActiveOfUser
    {//luis - 2011-11-02
        private static int _nUser;
        private static string _sName;
        public static int nUser
        {
            get { return _nUser; }
            set
            {
                int nUserOld = _nUser;
                _nUser = value;
                if (_nUser != 0 && nUserOld != _nUser) ORM.Current.ProductGetActiveOfUserAllAsync(_nUser, sName);
            }
        }
        public static string sName
        {//luis - 2011-12-21
            get { return _sName; }
            set
            {
                string _sNameOld = _sName;
                _sName = value;
                if (_nUser != 0 && _sName != _sNameOld) ORM.Current.ProductGetActiveOfUserAllAsync(_nUser, _sName);
            }
        }
        private static Boolean HaveChanged = true;
        public static void Load()
        {
            if (HaveChanged)
            {
                HaveChanged = false;
                //if (_nUser != 0) ORM.Current.ProductGetActiveOfUserAllAsync(null, null);
            }
        }
    }
}
