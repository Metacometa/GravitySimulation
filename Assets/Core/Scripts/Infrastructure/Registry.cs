using System.Collections.Generic;
using System.Linq;

namespace GravitySimulator.Infrastructure
{
    public class Registry<T>
    {
        public IReadOnlyCollection<T> Items => _items;
        
        private readonly HashSet<T> _items = new();
    
        public virtual void Add(T item) => _items.Add(item);
        public virtual void Remove(T item) => _items.Remove(item);

        public virtual T GetRandomValue()
        {
            int index = Random.Shared.Next(set.Count);;
            return _items.ElementAt(index);
        }
    }
}