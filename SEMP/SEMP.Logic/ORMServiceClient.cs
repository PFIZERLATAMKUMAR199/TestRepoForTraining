using System;
using System.Linq;
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
using SEMP.Controls;
using System.Reflection;
using System.ComponentModel;

namespace SEMP.Logic.WCF.Services
{
    public class ORM : ORMServiceClient
    {
        static ORM() { Current = new ORM(); }
        public static ORM Current { get; private set; }
        private ORM()
        {
            var events = this.GetType().GetEvents();
            var method = this.GetType().GetMethod("ORM_AsyncOperationCompleted", BindingFlags.NonPublic | BindingFlags.Static);
            foreach (var e in events)
                e.AddEventHandler(this,
                    Delegate.CreateDelegate(e.EventHandlerType, method));
            //this.CloseCompleted -= ORM_AsyncOperationCompleted;
            this.AsyncOperationCompleted -= ORM_AsyncOperationCompleted;
        }
        public event EventHandler<AsyncCompletedEventArgs> AsyncOperationCompleted;
        private static void ORM_AsyncOperationCompleted(Object Sender, AsyncCompletedEventArgs Args)
        {
            var hanlder = ((ORM)Sender).AsyncOperationCompleted;
            if (hanlder != null) hanlder(Sender, Args);
        }
    }
}
