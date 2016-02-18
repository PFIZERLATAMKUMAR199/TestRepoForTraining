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
    public class NamedEntityHelper
    {
        public IList Source { get; private set; }
        private static NamedEntity _Default = new NamedEntity();
        public static NamedEntity Default { get { return _Default; } }
        private static IList<NamedEntity> _DefaultList = new NamedEntity[] { Default };
        public static IList<NamedEntity> DefaultList { get { return _DefaultList; } }
        public void WCF_SetDefault(Object Sender, IEventArgs<ObservableCollection<NamedEntity>> Args)
        {
            Args.Result.Insert(0, _Default);
            Source = Args.Result;
        }
    }
}
