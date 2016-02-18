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

    public partial class RelativePage : RelativePageControl
    {
        public RelativePage()
            : base()
        {
            InitializeComponent();
            ORM.Current.RelativeGetCompleted += WCF_Get;
            ORM.Current.RelativeSaveCompleted += WCF_Save;
            ORM.Current.RelativeDeleteCompleted += WCF_Delete;
            _Filters.Add("Name", null);
            _Filters.Add("Kinship", null);
            _Filters.Add("RUT", null);
            _Filters.Add("BornDate", null);
        }
        override public void Load(CommandsMenu cmdMnu) 
        {//luis - 2012-07-19
            base.Load(cmdMnu);
            cmdMnu.NewAction = New_Click; 
        }
        private void New_Click()
        {//luis - 2012-07-19
            DataContext = new Relative() { SolicitorID = ((Relative)DataContext).SolicitorID };
        }

        private Dictionary<String, String> _Filters = new Dictionary<String, String>(4);
        protected override Dictionary<String, String> Filters
        {
            get
            {
                _Filters["Name"] = DataItem.Name == null ? null : DataItem.Name;
                _Filters["KinshipID"] = DataItem.KinshipID == null ? null : ((NamedEntity)cmbKinship.SelectedItem).Name;
                _Filters["RUT"] = DataItem.RUT == null ? null : DataItem.RUT;
                _Filters["BornDate"] = DataItem.BornDate == null ? null : ((DateTime)DataItem.BornDate).ToShortDateString();
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
                case "KinshipID":
                case "SolicitorID":
                case "Process":
        //        case "Status":  FRL - 2012-07-03
                    Args.Cancel = true;
                    break;
            }
        }

    }
}
