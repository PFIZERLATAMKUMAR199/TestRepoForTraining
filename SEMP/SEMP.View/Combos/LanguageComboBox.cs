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

namespace SEMP.View
{
    public class LanguageComboBox : BindableComboBox
    {
        private static IEnumerable Languages;
        static LanguageComboBox()
        {
            ORM.Current.LanguageGetAllCompleted += WCF_LanguageGetAllCompleted;
            SEMP.Logic.WCF.Services.Language.Load();
        }
        private static void WCF_LanguageGetAllCompleted(Object Sender, LanguageGetAllCompletedEventArgs Args)
        {
            Languages = Args.Result;
        }
        public LanguageComboBox()
        {
            ORM.Current.LanguageGetAllCompleted += WCF_LanguageGetAllCompleted_This;
            SelectedValuePath = "ID";
            DisplayMemberPath = "Name";
            ItemsSource = Languages;
            SelectedValue = 1;
        }
        private void WCF_LanguageGetAllCompleted_This(Object Sender, LanguageGetAllCompletedEventArgs Args)
        {
            ItemsSource = Languages;
            SelectedValue = 1;
        }
    }
}
