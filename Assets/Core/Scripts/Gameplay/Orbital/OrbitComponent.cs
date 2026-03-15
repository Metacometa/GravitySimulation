using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class OrbitComponent : MonoBehaviour
    {
        public OrbitBody Body => body;
        public OrbitPath Path => path;
        public OrbitView View => view;

        public Rigidbody2D Rb => _rb;

        [Header("Component References")]
        [SerializeField] private OrbitBody body;
        [SerializeField] private OrbitPath path;
        [SerializeField] private OrbitView view;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            body.Initialize(this);
            path.Initialize(this);
            view.Initialize(this);
        }
    }
}