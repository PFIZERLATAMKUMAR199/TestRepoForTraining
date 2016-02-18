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
using System.Collections.Generic;

namespace SEMP.View
{
    public class NamedEntitySHelper
    {
        public IList Source { get; private set; }
        private static NamedEntityS _Default = new NamedEntityS();
        public static NamedEntityS Default { get { return _Default; } }
        private static IList<NamedEntityS> _DefaultList = new NamedEntityS[] { Default };
        public static IList<NamedEntityS> DefaultList { get { return _DefaultList; } }
        public void WCF_SetDefault(Object Sender, IEventArgs<ObservableCollection<NamedEntityS>> Args)
        {
            Args.Result.Insert(0, _Default);
            Source = Args.Result;
        }
    }
}

