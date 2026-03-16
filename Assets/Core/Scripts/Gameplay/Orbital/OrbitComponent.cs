using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class OrbitComponent : MonoBehaviour
    {
        public OrbitBody Body => body;
        public OrbitPath Path => path;
        public OrbitEditor Editor => editor;
        public OrbitView View => view;

        public Vector2 Position => transform.position;

        public Rigidbody2D Rigidbody => _rb;

        [Header("Component References")]
        [SerializeField] private OrbitBody body;
        [SerializeField] private OrbitPath path;
        [SerializeField] private OrbitEditor editor;
        [SerializeField] private OrbitView view;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            body.Initialize(this);
            path.Initialize(this);
            editor.Initialize(this);
            view.Initialize(this);
        }
    }
}