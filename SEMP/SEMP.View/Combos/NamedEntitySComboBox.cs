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
using SEMP.Logic.WCF.Services;
using System.Collections;
using System.Collections.ObjectModel;

namespace SEMP.View
{
    public class NamedEntitySComboBox : BindableComboBox
    {
        public NamedEntitySComboBox()
        {
            SelectedValuePath = "ID";
            DisplayMemberPath = "Name";
        }
        protected void WCF_GetAll(Object Sender, IEventArgs<ObservableCollection<NamedEntityS>> Args) { Load(Args.Result); }
        protected virtual void Load(IEnumerable Source)
        {
            var selcted = SelectedValue;
            ItemsSource = Source;
            SelectedValue = selcted;
        }
    }
}
