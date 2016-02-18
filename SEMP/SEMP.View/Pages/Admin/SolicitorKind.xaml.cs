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
{//luis - 2011-10-25

    public partial class SolicitorKindPage : SolicitorKindPageControl
    {
        public SolicitorKindPage()
            : base()
        {
            InitializeComponent();
            ORM.Current.LanguageGetWordsCompleted += Language_GetWordsCompleted;
            ORM.Current.SolicitorKindGetCompleted += WCF_Get;
            ORM.Current.SolicitorKindSaveCompleted += WCF_Save;
            ORM.Current.SolicitorKindDeleteCompleted += WCF_Delete;
            //_Filters.Add("User", null);
            //_Filters.Add("Location", null);
        }

        public override void ConfigColumn(DataGridAutoGeneratingColumnEventArgs Args)
        {//luis - 2011-11-07
            
            var col = Args.Column;
            var bind = ((DataGridBoundColumn)col).Binding;
            switch (col.Header.ToString())
            {
                case "ID":
                case "RequestsWithoutStock":
                case "MaxProductAmmountPerRequest":
                case "MaxProductAmmountPerMonth":
                case "MaxValidTime":
                case "MaxRequestsPerMonth":
                case "Detail":
                case "Item": //luis - 2011-12-19
                case "Children":
                    Args.Cancel = true;
                    break;
            }
        }

        private Dictionary<String, String> _Filters = new Dictionary<String, String>(2);
        protected override Dictionary<String, String> Filters
        {
            get
            {
                //_Filters["User"] = DataItem.ID == null ? null : DataItem.Name;
                //_Filters["Location"] = ((NamedEntity)cmbLocation.SelectedItem).ID == null ? null : ((NamedEntity)cmbLocation.SelectedItem).Name;
                return _Filters;
            }
        }

        private void Language_GetWordsCompleted(Object Sender, LanguageGetWordsCompletedEventArgs Args)
        {
            grdChildren.Columns[1].Header = App.Lang["Product"];
            grdChildren.Columns[2].Header = App.Lang["AmountPerMonth"];
        }

    }
}
