using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SEMP.Logic;
using SEMP.Logic.WCF.Services;
using SEMP.Controls;
using System.ServiceModel;
using System.Threading;

namespace SEMP.View
{
    public partial class App : Application
    {
        public static LanguageDictionary Lang { get { return LanguageDictionary.Current; } }
        public static DictionaryConverter DictionaryConverter
        {
            get { return (DictionaryConverter)App.Current.Resources["DictionaryConverter"]; }
        }
        public App()
        {
            this.Exit += this.Application_Exit;
            this.Startup += this.Application_Startup;
            //this.UnhandledException += this.Application_UnhandledException;
            ORM.Current.Endpoint.Address = new EndpointAddress(new Uri(App.Current.Host.Source, "../ORMService.svc").AbsoluteUri);
            InitializeComponent();
            Resources.Add("AppLang", LanguageDictionary.Current);
            Resources.Add("BooleanConverter", BooleanConverter.Current);
            Resources.Add("VisibilityConverter", VisibilityConverter.Current);
            Resources.Add("DictionaryConverter", DictionaryConverter.Current);
            Resources.Add("ShortDateStringConverter", ShortDateStringConverter.Current); // FRL - 2011-12-06
            Resources.Add("MoneyStringConverter", MoneyStringConverter.Current); //luis - 2012-07-04

        }

        private static TimeSpan UpdateTodayDateDelay = TimeSpan.Parse("0:5:0.0");
        private static Timer UpdateTodayDateTimer = new Timer(
            delegate { Deployment.Current.Dispatcher.BeginInvoke(UpdateTodayDate); },
            null, Timeout.Infinite, Timeout.Infinite
        );
        private enum Months
        {
            January = 1, February, March, April, May, June,
            July, August, September, October, November, December
        }
        private static void UpdateTodayDate()
        {
            var d = DateTime.Today;
            var t = " | " + Lang[d.DayOfWeek.ToString()]
                  + ", " + Lang[Enum.GetName(typeof(Months), d.Month)]
                  + " " + d.ToString("dd, yyyy");
            if (Lang["TodayDate"] == null) Lang.Add("TodayDate", t);
            else Lang["TodayDate"] = t;
        }
        public static void UpdateLang(IEnumerable<Word> words)
        {
            Lang.DisableNotification();
            Lang.Clear();
            foreach (Word w in words)
                Lang.Add(w.Name, w.Description);
            UpdateTodayDate();
            Lang.EnableNotification();
            UpdateTodayDateTimer.Change(UpdateTodayDateDelay, UpdateTodayDateDelay);
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.RootVisual = new MainPage();
        }
        private void Application_Exit(object sender, EventArgs e)
        {
            ORM.Current.CloseAsync();
        }
        /*
        private void Application_UnhandledException(Object Sender, ApplicationUnhandledExceptionEventArgs Args)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Args.Handled = true;
                ReportErrorToDOM(e.ExceptionObject);
                //Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
        private void ReportErrorToDOM(Exception e)
        {
            try
            {
                string errorMsg = e.Message + e.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");
                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
                if (e.InnerException != null)
                    ReportErrorToDOM(e.InnerException);
            }
            catch { }
        }
        */
    }
}
