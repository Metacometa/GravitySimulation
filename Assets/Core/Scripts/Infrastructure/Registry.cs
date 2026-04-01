using System.Collections.Generic;

namespace GravitySimulator.Infrastructure
{
    public class Registry<T>
    {
        public IReadOnlyCollection<T> Items => _items;
        
        private readonly HashSet<T> _items = new();
    
        public virtual void Add(T item) => _items.Add(item);
        public virtual void Remove(T item) => _items.Remove(item);
    }
}