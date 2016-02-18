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
{// luis - 2011-10-24
    public class UserComboBox : NamedEntityComboBox
    {
        private static NamedEntityHelper Helper = new NamedEntityHelper();
        static UserComboBox() { ORM.Current.UserGetAllCompleted += Helper.WCF_SetDefault; }
        private static void This_Loaded(Object Sender, EventArgs Args) { User.Load(); }
        public UserComboBox()
        {
            Load(Helper.Source);
            ORM.Current.UserGetAllCompleted += WCF_GetAll;
            this.Loaded += This_Loaded;
        }
        protected override void Load(IEnumerable Source)
        {
            base.Load(Source);
            // <<-- FRL - 2012-01-03
            IsEnabled = true;
            if (Items.Count <= 2 && Items.Count != 0)
            {
                SelectedItem = Items[Items.Count - 1];
                IsEnabled = false;
            }
            // -->>
        }

    }
}
