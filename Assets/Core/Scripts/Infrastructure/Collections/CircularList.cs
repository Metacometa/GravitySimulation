using System;
using System.Collections.Generic;

namespace GravitySimulator.Infrastructure.Collections
{
    public class CircularList<T>
    {        
        public T this[int index] => _items[NormalizeIndex(index)];
        public IReadOnlyList<T> Items => _items;

        public int Count => _items.Count;

        private readonly List<T> _items = new();

        public T Get(int startIndex, int index) =>_items[NormalizeIndex(startIndex + index)];
        public int GetNextIndex(int index) => NormalizeIndex(index + 1);    
        
        public void Add(T item) => _items.Add(item);
        public void Remove(T item) => _items.Remove(item);
        public void Clear() => _items.Clear();        

        public IReadOnlyList<T> GetItemsFrom(int startIndex)
        {
            List<T> result = new();

            for (int i = 0; i < _items.Count; ++i)
            {
                result.Add(Get(startIndex, i));
            }

            return result;
        }

        private int NormalizeIndex(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException($"Index [{index}] is negative");

            int count = _items.Count;
            if (count == 0)
                throw new InvalidOperationException("Collection is empty");

            return index % count;            
        }
    }
}