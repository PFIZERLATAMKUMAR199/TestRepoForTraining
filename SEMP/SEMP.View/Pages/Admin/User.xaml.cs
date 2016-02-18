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
    public partial class UserPage : UserPageControl
    {//FRL - 2011-10-24

        public UserPage()
            : base()
        {
            InitializeComponent();
            ORM.Current.UserGetCompleted += WCF_Get;
            ORM.Current.UserSaveCompleted += WCF_Save;
            ORM.Current.UserDeleteCompleted += WCF_Delete;
            Clear();
            _Filters.Add("FirtName", null);
            _Filters.Add("LastName", null);
            _Filters.Add("EMail", null);
            _Filters.Add("Phone", null);
            _Filters.Add("Location", null);
            _Filters.Add("SolicitorKind", null);
            _Filters.Add("MaxProductAmmountPerMonth", null);
            _Filters.Add("RequestsForOtherUsers", null);
            _Filters.Add("OverloadsMaxAmmount", null);
            _Filters.Add("Active", null);
        }

        //public override void Load(CommandsMenu cmdMnu)
        //{//luis - 2012-06-28
        //    base.Load(cmdMnu);
        //    cmdMnu.NewAction = CommandsMenu.NoneAction;
        //}

        //This override is to hide columns from the datagrid
        public override void ConfigColumn(DataGridAutoGeneratingColumnEventArgs Args)
        {
            var col = Args.Column;
            var bind = ((DataGridBoundColumn)col).Binding;
            switch (col.Header.ToString())
            {
                case "ID":
                case "BornDate":
                case "Password":
                case "ProfileID":
                case "File":
                case "MaxProductAmmountPerMonth":
                case "DepartmentID":
                case "RequestsForOtherUsers":
                case "Phone":
                case "LocationID":
                case "OverloadsMaxAmmount":
                case "EmplID":
                case "SolicitorEmplID":
                case "FirstName":
                case "LastName":
                case "SolicitorKindID":
                case "Login":
                    Args.Cancel = true;
                    break;
                case "Name":
                    col.Header = "User";
                    col.DisplayIndex = 0;
                    break;
                case "EMail":
                    col.DisplayIndex = 0;
                    break;
            }
        }

        private Dictionary<String, String> _Filters = new Dictionary<String, String>(2);
        protected override Dictionary<String, String> Filters
        {
            get
            {
                _Filters["FirtName"] = DataItem.Name;
                _Filters["LastName"] = DataItem.LastName;
                _Filters["RequestsForOtherUsers"] = Convert.ToString(DataItem.RequestsForOtherUsers);
                _Filters["Active"] = Convert.ToString(DataItem.Active);

                return _Filters;
            }
        }

    }
}
