using GravitySimulator.Gameplay.Orbital.Trajectory.Mechanics;
using GravitySimulator.Gameplay.Orbital.Trajectory.Interaction;
using GravitySimulator.Gameplay.Orbital.Trajectory.View;
using GravitySimulator.Infrastructure.ComposableBehaviour;
using UnityEngine;
using GravitySimulator.Gameplay.Orbital.Body.Mechanics;

namespace GravitySimulator.Gameplay.Orbital.Trajectory.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class OrbitRoot : ComposableRoot
    {
        public Orbit Orbit => orbit;
        public OrbitPath Path => path;
        public OrbitBodyMotion Motion => bodyMotion;
        public OrbitEditor Editor => editor;
        public OrbitView View => view;

        public Vector2 Position => transform.position;

        public Rigidbody2D Rigidbody => _rb;

        [Header("Component References")]
        [SerializeField] private Orbit orbit;
        [SerializeField] private OrbitPath path;
        [SerializeField] private OrbitBodyMotion bodyMotion;
        [SerializeField] private OrbitEditor editor;
        [SerializeField] private OrbitView view;

        private Rigidbody2D _rb;

        public override void Initialize()
        {
            _rb = GetComponent<Rigidbody2D>();

            orbit.Initialize(this);
            path.Initialize(this);
            bodyMotion.Initialize(this);
            editor.Initialize(this);
            view.Initialize(this);
        }
        
        public override void Bind()
        {
            orbit.Bind();
            path.Bind();
            bodyMotion.Bind();
            editor.Bind();
            view.Bind();
        }

        public override void Activate()
        {
            orbit.Activate();
            path.Activate();
            bodyMotion.Activate();
            editor.Activate();
            view.Activate();
        }
    }
}