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

namespace SEMP.Controls
{
    public partial class FieldHelp : DataGrid
    {
        public FieldHelp()
        {
            InitializeComponent();
            CellStyle = (Style)Resources["CellWithOutBorder"];
            RowStyle = (Style)Resources["GridRowStyleNoSelected"];
        }
        private void _KeyDown(Object Sender, KeyEventArgs Args)
        {/*
            if (Args.Key == Key.C && IsCtrlPressed)
                Clipboard.Copy(Args, ((Item)((DataGrid)Sender).SelectedItem).);
        */
        }
        private static Boolean IsCtrlPressed
        {
            get
            {
                return ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control
                       || (Keyboard.Modifiers & ModifierKeys.Apple) == ModifierKeys.Apple);
            }
        }

        #region DoubleClick
        public event MouseButtonEventHandler DoubleClick;
        private DateTime _DoubleClickLimit = DateTime.MinValue;
        private Int32 _LastSelectedIndex = -1;
        private void _MouseLeftButtonUp(Object Sender, MouseButtonEventArgs Args)
        {
            var x = ItemsSource.GetEnumerator(); x.MoveNext();
            if (DateTime.Now < _DoubleClickLimit
            && _LastSelectedIndex == SelectedIndex
            && ((FrameworkElement)Args.OriginalSource).DataContext.GetType().Equals(x.Current.GetType()))
            {
                _DoubleClickLimit = DateTime.MinValue;
                var handler = DoubleClick;
                if (handler != null) handler(SelectedItem, Args);
            }
            else _DoubleClickLimit = DateTime.Now.AddMilliseconds(500);
            _LastSelectedIndex = SelectedIndex;
        }
        #endregion DoubleClick
    }
}
