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
{// FRL - 2011-11-04
    public class SolicitorKindComboBox : NamedEntityComboBox
    {
        private static NamedEntityHelper Helper = new NamedEntityHelper();
        static SolicitorKindComboBox() { ORM.Current.SolicitorKindGetAllCompleted += Helper.WCF_SetDefault; }
        private static void This_Loaded(Object Sender, EventArgs Args) { SolicitorKind.Load(); }
        public SolicitorKindComboBox()
        {
            Load(Helper.Source);
            ORM.Current.SolicitorKindGetAllCompleted += WCF_GetAll;
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
                    //                  <<--- FRL - 2011-11-24 : This is to solve the issue of the combobox loading AFTER the bound value is set
                    if (selected == null)
                    {
                        if (SelectedValue == null)
                        {//If the selected item is nothing and the selectedvalue is too, then we set the default option
                            SelectedItem = list.Count != 0 ? list[0] : null; // FRL - 2011-11-30 : fixed typo
                        }
                        else
                        {//if the selectedItem is null yet the selectedvalue was already set, then we must refresh it for the combobox to update
                            var temp = SelectedValue;
                            SelectedValue = null;
                            SelectedValue = temp;
                        }
                    }
                    else
                    {
                        SelectedItem = selected;
                    }
                    //selected = (selected == null ? (list.Count != 0 ? list[0] : null) : selected);
                    //SelectedItem = selected;
                    //                    -->>
                    IsEnabled = true;
                }
            }
        }
    }
}
