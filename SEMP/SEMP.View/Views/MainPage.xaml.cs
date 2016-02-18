using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using SEMP.Controls;
using SEMP.View.Pages;
using SEMP.Logic.WCF.Services;
using System.Collections;
using System.Windows.Browser;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Controls.Primitives;

namespace SEMP.View
{
    public partial class MainPage : UserControl
    {
        private IPage Page;
        private String User = String.Empty;

  
        private Boolean IsLogged { get { return User != String.Empty; } }
        private FieldHelp FieldHelp = new FieldHelp() { IsReadOnly = true /*FRL - 2012-06-28 */};
        private ExceptionListPresenter ExceptionsView = new ExceptionListPresenter();
  
        public MainPage()
        {
            
            App.Current.UnhandledException += App_UnhandledException;
            ORM.Current.LanguageGetWordsCompleted += Language_GetWordsCompleted;
            ORM.Current.AsyncOperationCompleted += Current_AsyncOperationCompleted;
            InitializeComponent();
            MenuCommands.ActionFired += MenuCommands_ActionFired;
            FieldHelp.DoubleClick += FieldHelp_DoubleClick;
            FieldHelp.AutoGeneratingColumn += FieldHelp_AutoGeneratingColumn;
            ExceptionsView.PropertyChanged += ExceptionsView_PropertyChanged;
            InitMainMenu(MainMenu.Items);
            DataContext = this;
            ShowLogin();
        }
      

        #region Language
        private void LanguageComboBox_SelectionChanged(Object Sender, SelectionChangedEventArgs Args)
        {
            ((Language)((ComboBox)Sender).SelectedItem).GetWords();
        }
        private void Language_GetWordsCompleted(Object Sender, LanguageGetWordsCompletedEventArgs Args)
        {
            App.UpdateLang(Args.Result);
            App.Lang.Add("PageTitle", Page == null ? (IsLogged ? null : App.Lang["LogIn"]) : App.Lang[Page.GetType().Name]);
            App.Lang["UserWelcome"] += User;
            UpdateLangMainMenu(MainMenu.Items);
            if(txtFilters.Visibility == Visibility.Visible)
                UpdateLangFieldHelp();
            NamedEntityHelper.Default.Name = App.Lang["[None]"].ToString();
        }
        private void UpdateLangMainMenu(IEnumerable options)
        {
            foreach (Option o in options)
            {
                UpdateLangMainMenu(o);
                o.Tag = App.Lang[o.Control is IPage ? o.Control.GetType().Name : o.Name];
            }
        }
        #endregion Language

        #region Login
        private void Logout_Click(Object Sender, EventArgs Args) { ShowLogin(); }
        private void ShowLogin()
        {
            ORM.Current.ClearSessionAsync();
            SetMainMenuVisibility(MainMenu.Items, null);
            ClearExceptions();
            btnLogout.Visibility = Visibility.Collapsed;
            Page = null;
            FieldHelp.ItemsSource = null;
            App.Lang["PageTitle"] = App.Lang["LogIn"];
            if (IsLogged) App.Lang["UserWelcome"] = App.Lang["UserWelcome"].ToString().Replace(User, String.Empty);
            txtFilters.Text = String.Empty;
            User = String.Empty;
            ShowPage();
        }
        private void LogInDone(Object Sender, EventArgs<ObservableCollection<String>> UserMenu)
        {
            //Author: Neha Upadhyay
            //Added to show disclaimer after user login
            myPopup.Show();
            ClearExceptions();
            User = UserMenu.Result[0];
            App.Lang["UserWelcome"] = App.Lang["UserWelcome"].ToString().Replace(User, String.Empty) + User;
            SetMainMenuVisibility(MainMenu.Items, UserMenu.Result);
            btnLogout.Visibility = Visibility.Visible;
            PageContainer.Child = null;
            App.Lang["PageTitle"] = String.Empty;
               
            
        }
        #endregion Login

        #region ExceptionHandling
        private void ClearExceptions() { ExceptionsView.Exceptions.Clear(); }
        private String LastException
        {
            get
            {
                var maxLength = 45;
                var last = ExceptionsView.Exceptions.Count == 0 ? String.Empty : ExceptionsView.Exceptions.Last().Message;
                if (last.Length > maxLength) last = last.Substring(0, maxLength) + "[...]";
                return last;
            }
        }
        private void ExceptionsView_PropertyChanged(Object Sender, PropertyChangedEventArgs Args)
        {
            btnError.Visibility = ExceptionsView.Exceptions.Count != 0 && PageContainer.Child != ExceptionsView
                ? Visibility.Visible : Visibility.Collapsed;
        }
        private void App_UnhandledException(Object Sender, ApplicationUnhandledExceptionEventArgs Args)
        {
            Args.Handled = true;
            var e = Args.ExceptionObject;
            ExceptionsView.Exceptions.Insert(0,
                e is TargetInvocationException
                ? e.InnerException
                : (App.Lang[e.Message] == null
                ? e : new Exception(App.Lang[e.Message].ToString(), e)));
            ((SolidColorBrush)txtLastEvent.Foreground).Color = Colors.Red;
            //neha 19 oct
            if (LastException.Equals("Sequence contains no elements"))
            {
                txtLastEvent.Text = "Si usted es un usuario válido intente acceder a las 24 horas "; //If you are a valid user try accessing after 24 hours
                txtLastEvent2.Text = LastException;
                btnError.Visibility = Visibility.Collapsed;
            }
            else
                txtLastEvent.Text = "El sistema ha detectado un error. Inténtalo más tarde";
            txtLastEvent2.Text = LastException;
        }
        #endregion ExceptionHandling

        #region ViewMenu
        private void MenuCommands_ActionFired(Object Sender, EventArgs<String> Args)
        {
            ClearExceptions();
            LastActionFired = Args.Result;
            ((SolidColorBrush)txtLastEvent.Foreground).Color = Colors.Blue;
            switch (LastActionFired)
            {
                case "Find": txtLastEvent.Text = App.Lang["Finding"].ToString(); break;
                case "Save": txtLastEvent.Text = App.Lang["Saving"].ToString(); break;
                case "Delete": txtLastEvent.Text = App.Lang["Deleting"].ToString(); break;
                case "Report": txtLastEvent.Text = App.Lang["Reporting"].ToString(); break;
                default: txtLastEvent.Text = String.Empty;
                    txtLastEvent2.Text = String.Empty; break;
            }
        }
        private String LastActionFired = String.Empty;
        private void Current_AsyncOperationCompleted(Object Sender, AsyncCompletedEventArgs Args)
        {
            if (Args.Error != null) throw Args.Error;
            ((SolidColorBrush)txtLastEvent.Foreground).Color = Colors.Green;
            switch (LastActionFired)
            {
                case "Find": txtLastEvent.Text = App.Lang["Found"].ToString(); break;
                case "Save": txtLastEvent.Text = App.Lang["Saved"].ToString(); break;
                case "Delete": txtLastEvent.Text = App.Lang["Deleted"].ToString(); break;
                case "Report": txtLastEvent.Text = App.Lang["Reported"].ToString(); break;
                default: txtLastEvent.Text = String.Empty;
                    txtLastEvent2.Text = String.Empty; break;
            }
            LastActionFired = String.Empty;
        }
        private void ClearCommands()
        {
            MenuCommands.Clear();
            btnError.Visibility = ExceptionsView.Exceptions.Count != 0 ? Visibility.Visible : Visibility.Collapsed;
            btnFind.Visibility = IsLogged && FieldHelp.ItemsSource != null ? Visibility.Visible : Visibility.Collapsed;
            btnPage.Visibility = Visibility.Visible;
            txtFilters.Visibility = Visibility.Collapsed;
        }
        private void ErrorView_Click(Object Sender, EventArgs Args) { ShowErrors(); }
        private void ShowErrors()
        {
            ClearCommands();
            btnError.Visibility = Visibility.Collapsed;
            if (PageContainer.Child != ExceptionsView)
                PageContainer.Child = ExceptionsView;
        }
        private void FindView_Click(Object Sender, EventArgs Args) { ShowFind(); }
        private void ShowFind()
        {
            ClearCommands();
            btnFind.Visibility = Visibility.Collapsed;
            txtFilters.Visibility = Visibility.Visible;
            if (PageContainer.Child != FieldHelp)
                PageContainer.Child = FieldHelp;
        }
        private void PageView_Click(Object Sender, EventArgs Args) { ShowPage(); }
        private void ShowPage()
        {
            ClearCommands();
            btnPage.Visibility = Visibility.Collapsed;
            if (IsLogged)
            {
                if (PageContainer.Child != Page)
                {
                    PageContainer.Child = (UIElement)Page;
                    Page.Load(MenuCommands);
                }
            }
            else
            {
                if (PageContainer.Child != Login)
                    PageContainer.Child = Login;
            }
        }
        #endregion ViewMenu

        #region MainMenu
        protected void SetMainMenuVisibility(IEnumerable options, IList<String> UserMenu)
        {
            foreach (Option o in options)
            {
                SetMainMenuVisibility(o, UserMenu);
                if (UserMenu == null)
                {
                    o.Visibility = Visibility.Collapsed;
                    if (o.Control is IPage) ((IPage)o.Control).Clear();
                }
                else
                    o.Visibility = UserMenu.Contains(o.Control == null ? o.Name : o.Control.GetType().Name)
                                 ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        private void InitMainMenu(IEnumerable options)
        {
            foreach (Option o in options)
            {
                InitMainMenu(o);
                if (o.Control is IPage)
                {
                    o.Executed += Option_Click;
                    if (o.Control is IABMPage)
                        ((IABMPage)o.Control).ItemFound += Page_ItemFound;
                }
            }
        }
        private void Option_Click(Object o, EventArgs e)
        {
            Page = (IPage)((Option)o).Control;
            if (PageContainer.Child != Page)
            {
                App.Lang["PageTitle"] = App.Lang[Page.GetType().Name];
                txtLastEvent.Text = String.Empty;
                txtLastEvent2.Text = String.Empty;
                FieldHelp.ItemsSource = null;
                ClearExceptions();
                ShowPage();
            }
        }
        #endregion MainMenu

        #region FieldHelp
        private void FieldHelp_AutoGeneratingColumn(Object Sender, DataGridAutoGeneratingColumnEventArgs Args)
        {
            ((IABMPage)Page).ConfigColumn(Args);
            if (!Args.Cancel)
            {
                var head = Args.Column.Header.ToString();
                if (head.Contains("Description") && head != "Description")
                    head = head.Replace("Description", String.Empty);
                if (head.Contains("Name") && head != "Name")
                    head = head.Replace("Name", String.Empty);
                // <<-- FRL - 2012-06-28
                if (head.Contains("Process") && head != "Process")
                    head = head.Replace("Process", String.Empty);
                // -->>
                FieldHelp_ColumnsHeaders.Add(head);
                Args.Column.Header = App.Lang[head];
            }
        }
        private void FieldHelp_DoubleClick(Object Sender, MouseButtonEventArgs Args)
        {
            ((IABMPage)Page).ItemFoundSelected(Sender);
            ShowPage();
        }
        private void Page_ItemFound(Object Sender, ItemFoundEventArgs Args)
        {
            FieldHelp_ColumnsHeaders.Clear();
            FieldHelp.ItemsSource = Args.Result;
            FieldHelp_Filters = Args.Filters;
            UpdateLangFieldHelp();
            ShowFind();
        }
        private List<String> FieldHelp_ColumnsHeaders = new List<String>();
        private Dictionary<String, String> FieldHelp_Filters;
        private void UpdateLangFieldHelp()
        {
            var cols = FieldHelp.Columns;
            for (var i = 0; i < cols.Count; i++)
                cols[i].Header = App.Lang[FieldHelp_ColumnsHeaders[i]];
            txtFilters.Text = App.Lang["Filters_c"].ToString();
            foreach (var kv in FieldHelp_Filters)
                if (!String.IsNullOrEmpty(kv.Value))
                    txtFilters.Text += " - " + App.Lang[kv.Key].ToString() + ": \"" + kv.Value + "\"";
        }
        #endregion FieldHelp
    }
}
