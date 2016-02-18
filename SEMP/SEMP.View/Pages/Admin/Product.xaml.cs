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

    public partial class ProductPage : ProductPageControl
    {
        public ProductPage()
            : base()
        {
            InitializeComponent();
            ORM.Current.ProductGetCompleted += WCF_Get;
            ORM.Current.ProductSaveCompleted += WCF_Save;
            ORM.Current.ProductDeleteCompleted += WCF_Delete;
        }

        //public override void Load(CommandsMenu cmdMnu)
        //{//luis - 2012-06-28
        //    base.Load(cmdMnu);
        //    cmdMnu.NewAction = CommandsMenu.NoneAction;
        //}

        public override void ConfigColumn(DataGridAutoGeneratingColumnEventArgs Args)
        {//luis - 2011-11-07

            var col = Args.Column;
            var bind = ((DataGridBoundColumn)col).Binding;
            switch (col.Header.ToString())
            {
                case "ID":
                case "Active":
                case "ColdChain":
                case "MonthlyAmount":
                case "AnnualAmount":
                case "UnitPackage":
                case "Detail":
                case "Item":
                case "ReplenishPoint":
                case "MaxAge":
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
                return _Filters;
            }
        }

    }
}
