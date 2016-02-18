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
{//FRL 2011-10-24

    public partial class RejectionReasonPage : RejectionReasonPageControl
    {
        public RejectionReasonPage()
            : base()
        {
            InitializeComponent();
            ORM.Current.RejectionReasonGetCompleted += WCF_Get;
            ORM.Current.RejectionReasonSaveCompleted += WCF_Save;
            ORM.Current.RejectionReasonDeleteCompleted += WCF_Delete;
            _Filters.Add("Name", null);
        }

        //This override is to hide columns from the datagrid
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
