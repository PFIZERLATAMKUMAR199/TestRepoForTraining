using System;

namespace SEMP.Controls
{
    public interface IEventArgs { Object Object { get; } }
    public interface IEventArgs<T> { T Result { get; } }
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T result) { Result = result; }
        public T Result { get; protected set; }
    }
}
