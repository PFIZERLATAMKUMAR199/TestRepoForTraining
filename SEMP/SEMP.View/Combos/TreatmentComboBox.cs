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
{//luis - 2012-06-21
    public class TreatmentComboBox : NamedEntityComboBox
    {
        private static NamedEntityHelper Helper = new NamedEntityHelper();
        static TreatmentComboBox() { ORM.Current.TreatmentGetAllCompleted += Helper.WCF_SetDefault; }
        private static void This_Loaded(Object Sender, EventArgs Args) { Treatment.Load(); }
        public TreatmentComboBox()
        {
            Load(Helper.Source);
            ORM.Current.TreatmentGetAllCompleted += WCF_GetAll;
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
