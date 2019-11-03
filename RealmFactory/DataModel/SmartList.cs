using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.RealmFactory.DataModel
{
    public delegate void ItemChangeHandler<T>(T item, int index);

    public class SmartList<T> : IList<T>
    {
        private List<T> nestedList;

        public SmartList()
        {
            nestedList = new List<T>();
        }

        public event ItemChangeHandler<T> ItemAdded;

        public event ItemChangeHandler<T> ItemRemoved;

        public event ItemChangeHandler<T> ItemModified;

        public event EventHandler ListModified;

        public void OnItemAdded(T item, int index)
        {
            if (ItemAdded != null) { ItemAdded(item, index); }
            OnListModified();
        }

        public void OnItemRemoved(T item, int index)
        {
            if (ItemRemoved != null) { ItemRemoved(item, index); }
            OnListModified();
        }

        public void OnItemModified(T item, int index)
        {
            if (ItemModified != null) { ItemModified(item, index); }
            OnListModified();
        }

        public void OnListModified()
        {
            if (ListModified != null) { ListModified(this, EventArgs.Empty); }
        }

        public int IndexOf(T item)
        {
            return nestedList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            nestedList.Insert(index, item);
            OnItemAdded(item, index);
        }

        public void RemoveAt(int index)
        {
            T item = nestedList[index];
            nestedList.RemoveAt(index);
            OnItemRemoved(item, index);
        }

        public T this[int index]
        {
            get
            {
                return nestedList[index];
            }
            set
            {
                nestedList[index] = value;
                OnItemModified(value, index);
            }
        }

        public void Add(T item)
        {
            nestedList.Add(item);
            OnItemAdded(item, nestedList.Count - 1);
        }

        public void Clear()
        {
            nestedList.Clear();
            OnListModified();
        }

        public bool Contains(T item)
        {
            return nestedList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            nestedList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return nestedList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            int index = nestedList.IndexOf(item);
            bool result = nestedList.Remove(item);

            if (index != -1)
            {
                OnItemRemoved(item, index);
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return nestedList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return nestedList.GetEnumerator();
        }
    }
}
