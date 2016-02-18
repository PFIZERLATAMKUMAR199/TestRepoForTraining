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
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SEMP.Controls
{
    public class ObservableDictionary : ObservableDictionary<Object, Object> { }
    public class ObservableDictionary<TKey, TValue>
        : INotifyPropertyChanged, INotifyCollectionChanged
        , IDictionary, IDictionary<TKey, TValue>
        , IEnumerable, IEnumerable<KeyValuePair<TKey, TValue>>
        , ICollection, ICollection<KeyValuePair<TKey, TValue>>
    {
        public ObservableDictionary()
        {
            Dictionary = new Dictionary<TKey, TValue>();
            IsCollectionNotificationEnabled = true;
            IsPropertyNotificationEnabled = true;
        }
        public Dictionary<TKey, TValue> Dictionary { get; private set; }

        #region IDictionary Members
        ICollection IDictionary.Keys { get { return ((IDictionary)Dictionary).Keys; } }
        ICollection IDictionary.Values { get { return ((IDictionary)Dictionary).Values; } }
        public Boolean IsFixedSize { get { return ((IDictionary)Dictionary).IsFixedSize; } }
        public Object this[Object key]
        {
            get { return ((IDictionary)Dictionary)[key]; }
            set
            {
                var old = ((IDictionary)Dictionary)[key];
                if (old != value)
                {
                    ((IDictionary)Dictionary)[key] = value;
                    RaiseCollectionChangedReplace(old, value);
                    RaisePropertyChanged(key);
                }
            }
        }
        public void Add(Object key, Object value)
        {
            ((IDictionary)Dictionary).Add(key, value);
            RaiseCollectionChangedAdd(((IDictionary)Dictionary)[key]);
            RaisePropertyChanged(key);
        }
        public void Remove(Object key)
        {
            var item = ((IDictionary)Dictionary)[key];
            ((IDictionary)Dictionary).Remove(key);
            RaiseCollectionChangedRemove(item);
            RaisePropertyChanged(key);
        }
        public Boolean Contains(Object key)
        {
            return ((IDictionary)Dictionary).Contains(key);
        }
        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)Dictionary).GetEnumerator();
        }
        #endregion IDictionary Members

        #region IDictionary<TKey, TValue> Members
        public TValue this[TKey key]
        {
            get { return Dictionary[key]; }
            set
            {
                var old = Dictionary[key];
                if (Object.Equals(old, value))
                {
                    Dictionary[key] = value;
                    RaiseCollectionChangedReplace(old, value);
                    RaisePropertyChanged(key);
                }
            }
        }
        public void Add(TKey key, TValue value)
        {
            Dictionary.Add(key, value);
            RaiseCollectionChangedAdd(Dictionary[key]);
            RaisePropertyChanged(key);
        }
        public Boolean Remove(TKey key)
        {
            var item = Dictionary[key];
            var result = Dictionary.Remove(key);
            RaiseCollectionChangedRemove(item);
            RaisePropertyChanged(key);
            return result;
        }
        public Boolean ContainsKey(TKey key)
        {
            return Dictionary.ContainsKey(key);
        }
        public Boolean TryGetValue(TKey key, out TValue value)
        {
            return Dictionary.TryGetValue(key, out value);
        }
        public ICollection<TKey> Keys
        {
            get { return ((ICollection<TKey>)Dictionary.Keys); }
        }
        public ICollection<TValue> Values
        {
            get { return ((ICollection<TValue>)Dictionary.Values); }
        }
        #endregion IDictionary<TKey, TValue> Members

        #region ICollection Members
        public Int32 Count { get { return Dictionary.Count; } }
        public Object SyncRoot { get { return ((ICollection)Dictionary).SyncRoot; } }
        public Boolean IsSynchronized { get { return ((ICollection)Dictionary).IsSynchronized; } }
        public Boolean IsReadOnly { get { return ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).IsReadOnly; } }
        public void CopyTo(Array array, Int32 index)
        {
            ((ICollection)Dictionary).CopyTo(array, index);
        }
        #endregion ICollection Members

        #region ICollection<KeyValuePair<TKey, TValue>> Members
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Add(item);
            RaiseCollectionChangedAdd(item);
            RaisePropertyChanged(item.Key);
        }
        public Boolean Remove(KeyValuePair<TKey, TValue> item)
        {
            Boolean result = ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Remove(item);
            RaiseCollectionChangedRemove(item);
            RaisePropertyChanged(item.Key);
            return result;
        }
        public void Clear()
        {
            foreach(var o in Keys) RaisePropertyChanged(o);
            Dictionary.Clear();
            RaiseCollectionChanged();
        }
        public Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Contains(item);
        }
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).CopyTo(array, arrayIndex);
        }
        #endregion ICollection<KeyValuePair<TKey, TValue>> Members

        #region IEnumerable Members
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }
        #endregion IEnumerable Members

        #region IEnumerable<KeyValuePair<TKey, TValue>> Members
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }
        #endregion IEnumerable<KeyValuePair<TKey, TValue>> Members

        #region Notification Control Members
        public Boolean IsPropertyNotificationEnabled { get; set; }
        public Boolean IsCollectionNotificationEnabled { get; set; }
        public void EnableNotification()
        {
            IsPropertyNotificationEnabled = true;
            IsCollectionNotificationEnabled = true;
        }
        public void DisableNotification()
        {
            IsPropertyNotificationEnabled = false;
            IsCollectionNotificationEnabled = false;
        }
        #endregion Notification Control Members

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(Object property)
        {
            if (!IsPropertyNotificationEnabled) return;
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new PropertyChangedEventArgs(property.ToString()));
                propertyChanged(this, new PropertyChangedEventArgs("Dictionary"));
            }
        }
        #endregion INotifyPropertyChanged Members

        #region INotifyCollectionChanged Members
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private void RaiseCollectionChanged()
        {
            if (!IsCollectionNotificationEnabled) return;
            NotifyCollectionChangedEventHandler collectionChanged = CollectionChanged;
            if ((collectionChanged != null))
                collectionChanged(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        private void RaiseCollectionChangedAdd(Object item)
        {
            RaisePropertyChanged("Count");
            if (!IsCollectionNotificationEnabled) return;
            NotifyCollectionChangedEventHandler collectionChanged = CollectionChanged;
            if ((collectionChanged != null))
                collectionChanged(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, 0));
        }
        private void RaiseCollectionChangedRemove(Object item)
        {
            RaisePropertyChanged("Count");
            if (!IsCollectionNotificationEnabled) return;
            NotifyCollectionChangedEventHandler collectionChanged = CollectionChanged;
            if ((collectionChanged != null))
                collectionChanged(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, 0));
        }
        private void RaiseCollectionChangedReplace(Object oldItem, Object newItem)
        {
            if (!IsCollectionNotificationEnabled) return;
            NotifyCollectionChangedEventHandler collectionChanged = CollectionChanged;
            if ((collectionChanged != null))
                collectionChanged(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem, 0));
        }
        #endregion INotifyCollectionChanged Members
    }
}
