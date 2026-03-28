using UnityEngine;

namespace GravitySimulator.Infrastructure.ComposableBehaviour
{
    public abstract class ComposableChild<TRoot> : MonoBehaviour
        where TRoot : ComposableRoot
    {
        public TRoot Root { get; private set; }     

        public virtual void Initialize(TRoot root) 
        { 
            Root = root;
        }

        public virtual void Bind() { }
        public virtual void Activate() { }
    }
}
