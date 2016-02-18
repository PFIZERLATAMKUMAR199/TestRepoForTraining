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
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace SEMP.Controls
{
    public class BindableComboBox : ComboBox
    {
        public BindableComboBox() : base()
        {
            base.SelectionChanged += This_SelectionChanged;
            // <<-- FRL - 2011-12-19
            //if (Searchable)
            //    base.KeyUp += SearchByKeyPress;
            // -->>
        }
        #region SelectedValueDependencyProperty
        public static readonly DependencyProperty SelectedValueProperty =
                DependencyProperty.Register("SelectedValue", typeof(Object), typeof(BindableComboBox),
                new PropertyMetadata(new PropertyChangedCallback(SelectedValuePropertyChanged)));
        private static void SelectedValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue) ((BindableComboBox)d).SelectedValue = (Object)e.NewValue;
        }
        public Object SelectedValue
        {
            get { return (Object)GetValue(BindableComboBox.SelectedValueProperty); }
            set
            {
                if (SelectedValuePath != null)
                {
                    var src = value == null
                            ? from item in Items
                              where null == item.GetType().GetProperty(SelectedValuePath).GetValue(item, null)
                              select item
                            : from item in Items
                              where value.Equals(item.GetType().GetProperty(SelectedValuePath).GetValue(item, null))
                              select item;
                    SelectedItem = src.Count() > 0 ? src.First() : null;
                }
            }
        }
        #endregion SelectedValueDependencyProperty
        #region SelectedValuePathDependencyProperty
        public static readonly DependencyProperty SelectedValuePathProperty =
                DependencyProperty.Register("SelectedValuePath", typeof(String), typeof(BindableComboBox),
                new PropertyMetadata(new PropertyChangedCallback(SelectedValuePathPropertyChanged)));
        private static void SelectedValuePathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue) ((BindableComboBox)d).SelectedValuePath = (String)e.NewValue;
        }
        public String SelectedValuePath
        {
            get { return (String)GetValue(BindableComboBox.SelectedValuePathProperty); }
            set { SetValue(BindableComboBox.SelectedValuePathProperty, value); }
        }
        #endregion SelectedValuePathDependencyProperty
        #region DisplayMemberDependencyProperty
        public static readonly DependencyProperty DisplayMemberProperty =
                DependencyProperty.Register("DisplayMember", typeof(String), typeof(BindableComboBox),
                new PropertyMetadata(String.Empty));
        public String DisplayMember
        {
            get { return (String)GetValue(BindableComboBox.DisplayMemberProperty); }
        }
        #endregion DisplayMemberDependencyProperty

        #region Search Logic

        private bool _bSearchable = true; // FRL - 2011-12-19
        // <<-- FRL - 2011-12-20
        public bool Searcheable
        {
            set
            {
                _bSearchable = value;
                if (_bSearchable) //If it's changed to searchable, then run logic for searching with dropdown
                {
                    foreach (ComboBoxItem item in this.Items)
                    {
                        item.KeyUp += delegate(object o, KeyEventArgs ke) { AutocompleteSearch(ke); };
                    }
                }
            }
            get { return _bSearchable; }
        }
        // -->>
        private void This_SelectionChanged(Object Sender, SelectionChangedEventArgs Args)
        {
            if (SelectedItem != null && !String.IsNullOrEmpty(SelectedValuePath))
            {
                SetValue(BindableComboBox.SelectedValueProperty,
                    SelectedItem.GetType().GetProperty(SelectedValuePath)
                    .GetValue(SelectedItem, null));
                SetValue(BindableComboBox.DisplayMemberProperty, Convert.ToString(
                    SelectedItem.GetType().GetProperty(DisplayMemberPath)
                    .GetValue(SelectedItem, null)));
            }
        }
        // <<-- FRL - 2011-12-19
        private string searchedString;
        private DateTime searchStart;
        
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (_bSearchable)
            {
                AutocompleteSearch(e);
            }
            //Now go on with regular KeyUp
            base.OnKeyUp(e);
        }

        //protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    //This logic allows to searcch when dropdown is active
        //    if (_bSearchable)
        //    {
        //        for (var itemIndex = 0; itemIndex < this.Items.Count; ++itemIndex )
        //        {
        //            var item =((ComboBoxItem) this.Items[itemIndex]);
        //            if(item != null)
        //                item.KeyUp += delegate(object o, KeyEventArgs ke) { AutocompleteSearch(ke); };
        //        }
        //    }
        //    base.OnItemsChanged(e);
        //}

        private void AutocompleteSearch(KeyEventArgs e)
        {
            //Since Key has only certain values, A-Z, D0-D9, NumPad0-9, Space, etc. 
            //let's just focus on letters, and numbers, and ignore all other keys... 
            //if they're pressed, clear the search history another option is to 
            //use PlatformKeyCode, but since it's platform specific, let's not.
            string key = e.Key.ToString();
            if (key.Length > 1 && (key.StartsWith("D") || key.StartsWith("NumPad")))
            { //remove the D/NumPad prefix to get the digit
                key = key.Replace("NumPad", "").Replace("D", "");
            }
            else if (key.Length > 1)
            {
                searchedString = "";
            }

            if (DateTime.Now.Subtract(searchStart).TotalMilliseconds < 1500) //If 1.5 seconds haven't elapsed from the last inputed character
            {   //search history is valid and has not yet expired...
                searchedString += key;
            }
            else
            {
                searchedString = key;
            }
            try
            {
                var foundItem = this.Items.First(searchStringInNamedIdentity());
                if (foundItem != null)
                {
                    this.SelectedItem = foundItem;
                }
            }
            catch { }
            searchStart = DateTime.Now;
        }

        private Func<object, bool> searchStringInNamedIdentity()
        {
            return x => {
                string sSearching = "";
                sSearching = x.GetType().GetProperty(this.DisplayMemberPath).GetValue(x, null).ToString();
                return sSearching.StartsWith(searchedString,StringComparison.CurrentCultureIgnoreCase);
            };
        }
        #endregion
        // -->>
    }
}
