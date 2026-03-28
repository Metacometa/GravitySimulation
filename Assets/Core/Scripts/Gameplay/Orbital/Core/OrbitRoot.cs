using GravitySimulator.Gameplay.Orbital.Interaction;
using GravitySimulator.Gameplay.Orbital.Mechanics;
using GravitySimulator.Gameplay.Orbital.View;
using GravitySimulator.Infrastructure.ComposableBehaviour;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class OrbitRoot : ComposableRoot
    {
        public Orbit Orbit => orbit;
        public OrbitPath Path => path;
        public OrbitMotion Motion => motion;
        public OrbitEditor Editor => editor;
        public OrbitView View => view;

        public Vector2 Position => transform.position;

        public Rigidbody2D Rigidbody => _rb;

        [Header("Component References")]
        [SerializeField] private Orbit orbit;
        [SerializeField] private OrbitPath path;
        [SerializeField] private OrbitMotion motion;
        [SerializeField] private OrbitEditor editor;
        [SerializeField] private OrbitView view;

        private Rigidbody2D _rb;

        public override void Initialize()
        {
            _rb = GetComponent<Rigidbody2D>();

            orbit.Initialize(this);
            path.Initialize(this);
            motion.Initialize(this);
            editor.Initialize(this);
            view.Initialize(this);
        }
        
        public override void Bind()
        {
            orbit.Bind();
            path.Bind();
            motion.Bind();
            editor.Bind();
            view.Bind();
        }

        public override void Activate()
        {
            orbit.Activate();
            path.Activate();
            motion.Activate();
            editor.Activate();
            view.Activate();
        }
    }
}