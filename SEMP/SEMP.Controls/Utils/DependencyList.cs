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

namespace SEMP.Controls
{
    public class DependencyList : DependencyList<Object> { }
    public class DependencyList<T> : DependencyObject
        , IList<T>, IList
        , ICollection<T>, ICollection
        , IEnumerable<T>, IEnumerable
    {
        #region Consructors
        public DependencyList() { List = new List<T>(); }
        public DependencyList(IEnumerable<T> list) { List = new List<T>(list); }
        #endregion Consructors

        private List<T> List;

        #region IList<T> Members
        public Int32 IndexOf(T item) { return List.IndexOf(item); }
        public void Insert(Int32 index, T item) { List.Insert(index, item); }
        public void RemoveAt(Int32 index) { List.RemoveAt(index); }
        public T this[Int32 index] { get { return List[index]; } set { List[index] = value; } }
        #endregion IList<T> Members

        #region IList Members
        public Int32 Add(Object value) { return ((IList)List).Add(value); }
        public Boolean Contains(Object value) { return ((IList)List).Contains(value); }
        public Int32 IndexOf(Object value) { return ((IList)List).IndexOf(value); }
        public void Insert(Int32 index, Object value) { ((IList)List).Insert(index, value); }
        public Boolean IsFixedSize { get { return ((IList)List).IsFixedSize; } }
        public void Remove(Object value) { ((IList)List).Remove(value); }
        object IList.this[Int32 index] { get { return ((IList)List)[index]; } set { ((IList)List)[index] = value; } }
        #endregion

        #region ICollection<T> Members
        public void Add(T item) { List.Add(item); }
        public void Clear() { List.Clear(); }
        public Boolean Contains(T item) { return List.Contains(item); }
        public void CopyTo(T[] array, Int32 arrayIndex) { List.CopyTo(array, arrayIndex); }
        public Int32 Count { get { return List.Count; } }
        public Boolean IsReadOnly { get { return ((ICollection<T>)List).IsReadOnly; } }
        public Boolean Remove(T item) { return List.Remove(item); }
        #endregion ICollection<T> Members

        #region ICollection Members
        public void CopyTo(Array array, Int32 index) { ((ICollection)List).CopyTo(array, index); }
        public Boolean IsSynchronized { get { return ((ICollection)List).IsSynchronized; } }
        public Object SyncRoot { get { return ((ICollection)List).SyncRoot; } }
        #endregion ICollection Members

        #region IEnumerable<T> Members
        public IEnumerator<T> GetEnumerator() { return List.GetEnumerator(); }
        #endregion IEnumerable<T> Members

        #region IEnumerable Members
        IEnumerator IEnumerable.GetEnumerator() { return ((IEnumerable)List).GetEnumerator(); }
        #endregion IEnumerable Members
    }
}
