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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SEMP.Logic.WCF.Services
{//Luis - 2011-12-19
    public partial class SolicitudeToPrintQuery
    {
        public void Report()
        {
            ORM.Current.SolicitudeToPrintQueryReportAsync(this);
        }

    }
}
