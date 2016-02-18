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
{// FRL - 2011-11-14
    public class SolicitudeStatusComboBox : NamedEntityComboBox
    {
        private static NamedEntityHelper Helper = new NamedEntityHelper();
        static SolicitudeStatusComboBox() { ORM.Current.SolicitudeStatusGetAllCompleted += Helper.WCF_SetDefault; }
        private static void This_Loaded(Object Sender, EventArgs Args) { SolicitudeStatus.Load(); }
        public SolicitudeStatusComboBox()
        {
            Load(Helper.Source);
            ORM.Current.SolicitudeStatusGetAllCompleted += WCF_GetAll;
            this.Loaded += This_Loaded;
        }
        protected override void Load(IEnumerable Source)
        {
            if (Source != null)
            {
                var list = (IList)Source;
                var selected = SelectedItem;
                ItemsSource = Source;
                if (list.Count == 2)
                {
                    SelectedItem = list[1];
                    IsEnabled = false;
                }
                else
                {
                    // Defaults the first option as selected to match other combos behaviour
                    selected = (selected == null ? (list.Count != 0 ? list[0] : null) : selected);
                    SelectedItem = selected;
                    IsEnabled = true;
                }
            }
        }
    }
}
