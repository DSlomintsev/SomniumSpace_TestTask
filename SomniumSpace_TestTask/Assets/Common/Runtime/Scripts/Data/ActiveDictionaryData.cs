using System;
using System.Collections.Generic;
using Common.Utils;

namespace Common.Data
{
    public class ActiveDictionaryData<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public event Action<Dictionary<TKey, TValue>> UpdateEvent;
        public event Action<TKey, TValue> AddEvent;
        public event Action<TKey, TValue> RemoveEvent;

        private Dictionary<TKey, TValue> value;

        public ActiveDictionaryData(Dictionary<TKey, TValue> list = null)
        {
            if (list != null)
                value = list;
            else
                value = new Dictionary<TKey, TValue>();
        }

        public new void Add(TKey key, TValue item)
        {
            value.Add(key, item);
            UpdateEvent.Call(value);
            AddEvent.Call(key, item);
        }

        public new bool Remove(TKey key)
        {
            var item = value[key];
            var result = value.Remove(key);
            if (result)
            {
                UpdateEvent.Call(value);
                RemoveEvent.Call(key, item);
            }
            return result;
        }

        public new int Count
        {
            get { return value.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public new void Clear()
        {
            foreach (var item in value)
            {
                RemoveEvent.Call(item.Key, item.Value);
            }

            value.Clear();
        }

        public new bool ContainsKey(TKey key)
        {
            return value.ContainsKey(key);
        }

        public new bool ContainsValue(TValue item)
        {
            return value.ContainsValue(item);
        }

        public virtual Dictionary<TKey, TValue> Value
        {
            get { return value; }
            set
            {
                if (this.value != null && !this.value.Equals(value))
                {
                    this.value = value;
                    UpdateEvent.Call(this.value);
                }
                else if (this.value == null)
                {
                    this.value = value;
                    UpdateEvent.Call(this.value);
                }
            }
        }
        
        public new TValue this[TKey key]
        {
            get { return value[key]; }
            set { this.value[key] = value; }
        }

        /*public new IEnumerator<T> GetEnumerator()
        {
            return value.GetEnumerator();
        }

        public new int IndexOf(T item)
        {
            return value.IndexOf(item);
        }*/

        /*public new void Insert(int index, T item)
        {
            value.Insert(index, item);
            UpdateEvent.Call(value);
        }*/

        /*public new T this[int index]
        {
            get { return value[index]; }
            set { this.value[index] = value; }
        }


        public new T Find(Predicate<T> match)
        {
            return Value.Find(match);
        }*/
    }
}