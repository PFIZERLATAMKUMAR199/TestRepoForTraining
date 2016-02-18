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
{//FRL 2011-11-11
    public partial class SolicitudeStatus : IPageData, ICloneable
    {
        static SolicitudeStatus()
        {
        }
        private static Boolean HaveChanged = true;
        public static void Load()
        {
            if (HaveChanged)
            {
                HaveChanged = false;
                ORM.Current.SolicitudeStatusGetAllAsync();
            }
        }
        public void Find() { ORM.Current.SolicitudeStatusGetAsync(this); }
        public void Save() { }
        public void Delete() { }
        public Object Clone() { return this; }
    }
}
