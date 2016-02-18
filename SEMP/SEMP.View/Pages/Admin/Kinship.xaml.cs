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
{//FRL 2012-06-12

    public partial class KinshipPage : KinshipPageControl
    {
        public KinshipPage()
            : base()
        {
            InitializeComponent();
            ORM.Current.KinshipGetCompleted += WCF_Get;
            ORM.Current.KinshipSaveCompleted += WCF_Save;
            ORM.Current.KinshipDeleteCompleted += WCF_Delete;
            _Filters.Add("Name", null);
            _Filters.Add("KinshipKind", null);
        }

        private Dictionary<String, String> _Filters = new Dictionary<String, String>(2);
        protected override Dictionary<String, String> Filters
        {
            get
            {
                _Filters["Name"] = DataItem.Name == null ? null : DataItem.Name;
                _Filters["KinshipKind"] = DataItem.KinshipKindID == null ? null : ((NamedEntity)cmbKinshipKind.SelectedItem).Name;
                return _Filters;
            }
        }
        public override void ConfigColumn(DataGridAutoGeneratingColumnEventArgs Args)
        {//FRL - 2012-06-14

            var col = Args.Column;
            var bind = ((DataGridBoundColumn)col).Binding;
            switch (col.Header.ToString())
            {
                case "ID":
                case "KinshipKindID":
                    Args.Cancel = true;
                    break;
            }
        }

    }
}
