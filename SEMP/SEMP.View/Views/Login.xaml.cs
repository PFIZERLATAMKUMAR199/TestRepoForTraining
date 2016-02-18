using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SEMP.Logic.WCF.Services;
using SEMP.Controls;
using System.Windows.Markup;

namespace SEMP.View
{
    public partial class Login : UserControl
    {
        public event EventHandler<EventArgs<ObservableCollection<String>>> LogIn;
        public Login()
        {
            ORM.Current.ClearSessionCompleted += Current_ClearSessionCompleted;
            ORM.Current.GetUserMenuCompleted += Current_GetUserMenuCompleted;
            InitializeComponent();
            DataContext = this;
        }
        private void Current_ClearSessionCompleted(Object Sender, ClearSessionCompletedEventArgs Args)
        {
            ORM.Current.ClearSessionCompleted -= Current_ClearSessionCompleted;
            var x = Args.Result.Split("\\".ToCharArray());
            if (x.Count() == 2)
            {
                if (txtDomn.Text == String.Empty) txtDomn.Text = x[0];
                if (txtUser.Text == String.Empty) txtUser.Text = x[1];
            }
        }
        private void Current_GetUserMenuCompleted(Object Sender, GetUserMenuCompletedEventArgs Args)
        {
            var handler = LogIn;
            if (LogIn != null) handler(this, new EventArgs<ObservableCollection<String>>(Args.Result));
        }
        private void _KeyDown(Object Sender, KeyEventArgs Args)
        {
            if (Args.Key == Key.Enter) Login_Click(null, null);
        }
      
        private void Login_Click(Object Sender, EventArgs Args)
        {
            
            ORM.Current.GetUserMenuAsync(txtDomn.Text, txtUser.Text, txtPass.Password);
            txtPass.Password = String.Empty;
            txtPass.Focus();
        }
    }
}
