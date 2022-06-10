using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColonizationMobileGame.Utility.Observing
{
    [Serializable]
    public class ObservingCollection<T> : ICollection<T>
        where T : IObservable
    {
        [SerializeField, HideInInspector] private List<T> list = new List<T>();
        
        public int Count => list.Count;
        public bool IsReadOnly => (list as IList<T>).IsReadOnly;
        
        public event Action Changed;
        
        
        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void Add(T item)
        {
            list.Add(item);

            if (item != null)
            {
                item.Changed += InvokeChangedEvent;
            }
        }


        public void Clear()
        {
            foreach (T item in list)
            {
                item.Changed -= InvokeChangedEvent;
            }
            
            list.Clear();
        }


        public bool Contains(T item)
        {
            return list.Contains(item);
        }


        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }


        public bool Remove(T item)
        {
            if (item != null)
            {
                item.Changed -= InvokeChangedEvent;
            }
            
            return list.Remove(item);
        }


        public T this[int index]
        {
            get => list[index];
            set
            {
                list[index].Changed -= InvokeChangedEvent;
                
                list[index] = value;
                
                list[index].Changed += InvokeChangedEvent;
            }
        }


        private void InvokeChangedEvent()
        {
            Changed?.Invoke();
        }
    }
}