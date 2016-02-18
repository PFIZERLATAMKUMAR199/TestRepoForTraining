using System;
using System.Windows.Input;

namespace SEMP.Controls
{
    interface IDelegateCommand : ICommand
    {
        Predicate<Object> CanExecuteDelegate { get; set; }
        Boolean IsEnabled { get; }
        event EventHandler Executed;
        void Execute();
    }
}
