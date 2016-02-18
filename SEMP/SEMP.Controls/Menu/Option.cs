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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace SEMP.Controls
{
    public class Option : List<Option>, ICommand, IDelegateCommand, INotifyPropertyChanged
    {
        #region Consructors
        public Option() { }
        public Option(IEnumerable<Option> options) : base(options) { }
        public Option(Object tag) { Tag = tag; }
        public Option(Object tag, IEnumerable<Option> options) : base(options) { Tag = tag; }
        public Option(UIElement control) { Control = control; }
        public Option(UIElement control, IEnumerable<Option> options) : base(options) { Control = control; }
        public Option(Object tag, UIElement control) { Tag = tag; Control = control; }
        public Option(Object tag, UIElement control, IEnumerable<Option> options) : base(options) { Tag = tag; Control = control; }
        #endregion Consructors

        #region TagNotificableProperty
        private Object _Tag;
        public Object Tag
        {
            get { return this._Tag; }
            set
            {
                if (_Tag != value)
                {
                    _Tag = value;
                    RaisePropertyChanged("Tag");
                }
            }
        }
        #endregion TagNotificableProperty
      
        #region NameNotificableProperty
        private String _Name;
        public String Name
        {
            get { return this._Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        #endregion NameNotificableProperty
        
        #region ControlNotificableProperty
        private UIElement _Control;
        public UIElement Control
        {
            get { return this._Control; }
            set
            {
                if (_Control != value)
                {
                    _Control = value;
                    RaisePropertyChanged("Control");
                }
            }
        }
        #endregion ControlNotificableProperty
        
        #region VisibilityNotificableProperty
        private Visibility _Visibility;
        public Visibility Visibility
        {
            get { return this._Visibility; }
            set
            {
                if (_Visibility != value)
                {
                    _Visibility = value;
                    RaisePropertyChanged("Visibility");
                }
            }
        }
        #endregion VisibilityNotificableProperty

        #region IDelegateCommand Members
        #region CanExecuteChanged
        public event EventHandler CanExecuteChanged;
        protected void RaiseCanExecuteChanged(Object Sender)
        {
            var handler = CanExecuteChanged;
            if (handler != null) handler(Sender, EventArgs.Empty);
        }
        #endregion CanExecuteChanged

        #region CanExecute
        private Predicate<Object> _CanExecuteDelegate;
        public Predicate<Object> CanExecuteDelegate
        {
            get { return _CanExecuteDelegate; }
            set
            {
                _CanExecuteDelegate = value;
                RaiseCanExecuteChanged(this);
            }
        }
        public Boolean IsEnabled { get { return CanExecute(this); } }
        public Boolean CanExecute(Object Sender)
        {
            var del = _CanExecuteDelegate;
            return del == null || del(Sender);
        }
        #endregion CanExecute

        #region Executed
        public event EventHandler Executed;
        public void Execute() { Execute(this); }
        public void Execute(Object Sender)
        {
            var handler = Executed;
            if (handler != null && CanExecute(Sender))
                handler(Sender, EventArgs.Empty);
        }
        #endregion Executed
        #endregion IDelegateCommand Members

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if ((handler != null)) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion INotifyPropertyChanged Members
    }
}
