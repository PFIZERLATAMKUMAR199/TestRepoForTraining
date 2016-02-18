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
{// luis - 2011-11-17
    public class ProductActiveComboBox : NamedEntityComboBox
    {
        private static NamedEntityHelper Helper = new NamedEntityHelper();
        static ProductActiveComboBox() { ORM.Current.ProductGetActiveAllCompleted += Helper.WCF_SetDefault; }
        private static void This_Loaded(Object Sender, EventArgs Args) { ProductActive.Load(); }
        public ProductActiveComboBox()
        {
            Load(Helper.Source);
            ORM.Current.ProductGetActiveAllCompleted += WCF_GetAll;
            this.Loaded += This_Loaded;
        }

        //FRL - 2011-11-29 : Fixes issues with bound items being loaded before the combo is
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
                    IsEnabled = true;
                }
            }
        }
    }
}
