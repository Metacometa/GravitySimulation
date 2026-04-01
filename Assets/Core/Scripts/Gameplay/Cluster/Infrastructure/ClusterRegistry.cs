using System.Collections.Generic;
using GravitySimulator.Infrastructure;

namespace GravitySimulator.Gameplay.Clusters
{
    public class ClusterRegistry : Registry<Cluster>
    {
        public IReadOnlyCollection<Cluster> Active => _active;
        
        private readonly HashSet<Cluster> _active = new();

        public override void Add(Cluster item)
        {
            base.Add(item);
            _active.Add(item);
        }

        public override void Remove(Cluster item)
        {
            base.Remove(item);
            RemoveActive(item);
        }

        public void RemoveActive(Cluster item)
        {
            _active.Remove(item);
        }
    }
}