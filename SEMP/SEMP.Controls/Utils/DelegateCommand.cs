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

namespace SEMP.Controls
{
    public class DelegateCommand : ICommand, IDelegateCommand
    {
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
    }
}
