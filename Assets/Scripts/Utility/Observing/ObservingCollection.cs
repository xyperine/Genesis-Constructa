using System;
using System.Collections;
using System.Collections.Generic;

namespace MoonPioneerClone.Utility.Observing
{
    [Serializable]
    public class ObservingCollection<T> : ICollection<T>
        where T : IObservable
    {
        private IList<T> _list = new List<T>();
        
        public int Count => _list.Count;
        public bool IsReadOnly => _list.IsReadOnly;
        
        public event Action Changed;
        
        
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void Add(T item)
        {
            _list.Add(item);

            if (item != null)
            {
                item.Changed += InvokeChangedEvent;
            }
        }


        public void Clear()
        {
            foreach (T item in _list)
            {
                item.Changed -= InvokeChangedEvent;
            }
            
            _list.Clear();
        }


        public bool Contains(T item)
        {
            return _list.Contains(item);
        }


        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }


        public bool Remove(T item)
        {
            if (item != null)
            {
                item.Changed -= InvokeChangedEvent;
            }
            
            return _list.Remove(item);
        }


        public T this[int index]
        {
            get => _list[index];
            set
            {
                _list[index].Changed -= InvokeChangedEvent;
                
                _list[index] = value;
                
                _list[index].Changed += InvokeChangedEvent;
            }
        }


        private void InvokeChangedEvent()
        {
            Changed?.Invoke();
        }
    }
}