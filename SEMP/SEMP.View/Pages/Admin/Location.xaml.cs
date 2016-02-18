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
using SEMP.Controls;
using SEMP.Logic.WCF.Services;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SEMP.View.Pages
{
    public partial class LocationPage : LocationPageControl
    {//luis 2012-07-05
        public LocationPage()
            : base()
        {
            InitializeComponent();
            ORM.Current.LocationGetCompleted += WCF_Get;
            ORM.Current.LocationSaveCompleted += WCF_Save;
            ORM.Current.LocationDeleteCompleted += WCF_Delete;
            _Filters.Add("Name", null);
        }
        public override void Load(CommandsMenu cmdMnu) { base.Load(cmdMnu); cmdMnu.CloneAction = CommandsMenu.NoneAction; }

        public override void ConfigColumn(DataGridAutoGeneratingColumnEventArgs Args)
        {
            var col = Args.Column;
            var bind = ((DataGridBoundColumn)col).Binding;
            switch (col.Header.ToString())
            {
                case "ID":
                    Args.Cancel = true;
                    break;
            }
        }

        private Dictionary<String, String> _Filters = new Dictionary<String, String>(2);
        protected override Dictionary<String, String> Filters
        {
            get
            {
                _Filters["Name"] = DataItem.Name == null ? null : DataItem.Name;
                return _Filters;
            }
        }
    }
}
