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
    public partial class CommandsMenu : StackPanel
    {
        public static Action NoneAction = new Action(() => { });
        public CommandsMenu()
        {
            InitializeComponent();
            Clear();
        }
        public void Clear()
        {
            NewAction = NoneAction;
            FindAction = NoneAction;
            SaveAction = NoneAction;
            CloneAction = NoneAction;
            DeleteAction = NoneAction;
            ReportAction = NoneAction;
            ReloadAction = NoneAction;
            CancelAction = NoneAction;
        }
        private void Command_Click(Object Sender, RoutedEventArgs Args)
        {
            this.UpdateBindingSource();
            var action = ((Button)Sender).Name.ToString();
            switch (action)
            {
                case "New": NewAction(); break;
                case "Find": FindAction(); break;
                case "Save": SaveAction(); break;
                case "Clone": CloneAction(); break;
                case "Delete": DeleteAction(); break;
                case "Report": ReportAction(); break;
                case "Reload": ReloadAction(); break; // FRL - 2012-01-09
                case "Cancel": CancelAction(); break;
            }
            RaiseActionFired(action);
        }
        public event EventHandler<EventArgs<String>> ActionFired;
        protected void RaiseActionFired(String Action)
        {
            var handler = ActionFired;
            if ((handler != null)) handler(this, new EventArgs<String>(Action));
        }
        private void HideIfNone(UIElement e, Action value)
        {
            e.Visibility = value == NoneAction ? Visibility.Collapsed : Visibility.Visible;
        }

        #region Actions
        #region New
        public Action _NewAction;
        public Action NewAction
        {
            get { return _NewAction; }
            set
            {
                HideIfNone(New, value);
                _NewAction = value;
            }
        }
        #endregion New

        #region Find
        public Action _FindAction;
        public Action FindAction
        {
            get { return _FindAction; }
            set
            {
                HideIfNone(Find, value);
                _FindAction = value;
            }
        }
        #endregion Find

        #region Save
        public Action _SaveAction;
        public Action SaveAction
        {
            get { return _SaveAction; }
            set
            {
                HideIfNone(Save, value);
                _SaveAction = value;
            }
        }
        #endregion Save

        #region Clone
        public Action _CloneAction;
        public Action CloneAction
        {
            get { return _CloneAction; }
            set
            {
                HideIfNone(Clone, value);
                _CloneAction = value;
            }
        }
        #endregion Clone

        #region Delete
        public Action _DeleteAction;
        public Action DeleteAction
        {
            get { return _DeleteAction; }
            set
            {
                HideIfNone(Delete, value);
                _DeleteAction = value;
            }
        }
        #endregion Delete

        #region Report
        public Action _ReportAction;
        public Action ReportAction
        {
            get { return _ReportAction; }
            set
            {
                HideIfNone(Report, value);
                _ReportAction = value;
            }
        }
        #endregion Report

        #region Cancel
        public Action _CancelAction;
        public Action CancelAction
        {
            get { return _CancelAction; }
            set
            {
                HideIfNone(Cancel, value);
                _CancelAction = value;
            }
        }
        #endregion Cancel

        // <<-- FRL - 2012-01-09
        #region Reload
        public Action _ReloadAction;
        public Action ReloadAction
        {
            get { return _ReloadAction; }
            set
            {
                HideIfNone(Reload, value);
                _ReloadAction = value;
            }
        }
        #endregion Reload
        // -->>
        #endregion Actions
    }
}
