using System;
using System.Collections;
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

namespace SEMP.Controls
{
    public partial class DataChildrenGrid : DataGrid
    {
        private DataGridColumn _ActionColumn;
        // <<-- FRL - 2011-10-17
        private bool bRowAddColumnVisible = true;
        public bool RowAddColumnVisible
        {
            get { return this.bRowAddColumnVisible; }
            set
            {
                this.bRowAddColumnVisible = value;
                this.Columns[0].Visibility = (this.bRowAddColumnVisible ? Visibility.Visible : Visibility.Collapsed);
            }
            // -->>
        }
        // <<-- - 2011-12-05
        private string sNewChildrenMethod = "NewChild";
        public string NewChildrenMethod
        {
            get { return sNewChildrenMethod; }
            set { sNewChildrenMethod = value; }
        }
        // -->>
        public DataChildrenGrid()
        {
            InitializeComponent();
            _ActionColumn = this.Columns[0];
            CellStyle = (Style)Resources["CellWithOutBorder"];
            RowStyle = (Style)Resources["GridRowStyleNoSelected"];
        }
        /*
        private void DataGrid_Loaded(Object Sender, EventArgs Args)
        {
            System.Threading.Thread.Sleep(1000);
            _ActionColumn.DisplayIndex = this.Columns.Count - 1;
        }
        */
        private void Children_Delete(Object Sender, EventArgs Args)
        {
            ((IList)this.ItemsSource).Remove(this.SelectedItem);
            //((IPageDataParent)DataContext)[GetChildrenName()] = (IList)this.ItemsSource;
        }
        private void Children_Add(Object Sender, EventArgs Args)
        {
            // <<-- FRL - 2011-12-05
            object[] ChildrenName = {GetChildrenName()};
            DataContext.GetType().GetMethod(sNewChildrenMethod).Invoke(DataContext, ChildrenName );
            // -->>
        }
        private String GetChildrenName()
        {
            return this.GetBindingExpression(DataGrid.ItemsSourceProperty).ParentBinding.Path.Path;
        }
    }
}
