using UnityEngine;

namespace GravitySimulator.Infrastructure.ComposableBehaviour
{
    public abstract class ComposableRoot : MonoBehaviour
    {
        public virtual void Initialize() {}
        public virtual void Bind() { }
        public virtual void Activate() { }
    }
}
