using System;
using System.Collections.Generic;
using Common.Utils;

namespace Common.Data
{
    [Serializable]
    public class ActiveListData<T> : List<T>
    {
        public event Action<List<T>> UpdateEvent;
        public event Action<T,int> AddEvent;
        public event Action<T,int> RemoveEvent;

        private List<T> value;

        public ActiveListData(List<T> list = null)
        {
            value = list ?? new List<T>();
        }

        public new void Add(T item)
        {
            value.Add(item);
            UpdateEvent.Call(value);
            AddEvent.Call(item,value.Count);
        }
        
        public void AddWithoutNotify(T item)
        {
            value.Add(item);
        }

        public void CallUpdated() => UpdateEvent.Call(value);

        public new void CopyTo(T[] array, int arrayIndex) => value.CopyTo(array, arrayIndex);

        public new bool Remove(T item)
        {
            var index = value.IndexOf(item);
            var result = value.Remove(item);
            if (result)
            {
                UpdateEvent.Call(value);
                RemoveEvent.Call(item,index);
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
            if (value.Count == 0) return;

            var index = 0;
            
            for(var i=0;i<value.Count;i++)
            {
                var item = value[i];
                RemoveEvent.Call(item,i);
            }

            value.Clear();
            UpdateEvent.Call(value);
        }

        public new bool Contains(T item)
        {
            return value.Contains(item);
        }

        public void AddRange(List<T> range)
        {
            value.AddRange(range);
            UpdateEvent.Call(value);
        }
        
        public new void AddRange(IEnumerable<T> range)
        {
            value.AddRange(range);
            UpdateEvent.Call(value);
        }

        public virtual List<T> Value
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

        public new IEnumerator<T> GetEnumerator()
        {
            return value.GetEnumerator();
        }


        public new int IndexOf(T item)
        {
            return value.IndexOf(item);
        }

        public new void Insert(int index, T item)
        {
            value.Insert(index, item);
            UpdateEvent.Call(value);
            AddEvent.Call(item,index);
        }

        public new void RemoveAt(int index)
        {
            var result = value[index];
            value.RemoveAt(index);
            UpdateEvent.Call(value);
            RemoveEvent.Call(result,index);
        }

        public new T this[int index]
        {
            get { return value[index]; }
            set { this.value[index] = value; }
        }


        public new T Find(Predicate<T> match)
        {
            return Value.Find(match);
        }

        public new void ForEach(Action<T> action)
        {
            value.ForEach(action);
        }
    }
}