using GravitySimulator.Gameplay.Orbital.Trajectory.Mechanics;
using GravitySimulator.Gameplay.Orbital.Trajectory.Interaction;
using GravitySimulator.Gameplay.Orbital.Trajectory.View;
using GravitySimulator.Infrastructure.ComposableBehaviour;
using UnityEngine;
using GravitySimulator.Gameplay.Orbital.Body.Mechanics;

namespace GravitySimulator.Gameplay.Orbital.Trajectory.Core
{
    public class OrbitRoot : ComposableRoot
    {
        public OrbitModel Orbit => model;
        public OrbitPath Path => path;
        public OrbitBodyMotion Motion => bodyMotion;
        public OrbitEditor Editor => editor;
        public OrbitView View => view;

        public Vector2 BodyPosition => bodyMotion.transform.position;

        [Header("Component References")]
        [SerializeField] private OrbitModel model;
        [SerializeField] private OrbitPath path;
        [SerializeField] private OrbitBodyMotion bodyMotion;
        [SerializeField] private OrbitEditor editor;
        [SerializeField] private OrbitView view;

        public override void Initialize()
        {
            model.Initialize(this);
            path.Initialize(this);
            bodyMotion.Initialize(this);
            editor.Initialize(this);
            view.Initialize(this);
        }
        
        public override void Bind()
        {
            model.Bind();
            path.Bind();
            bodyMotion.Bind();
            editor.Bind();
            view.Bind();
        }

        public override void Activate()
        {
            model.Activate();
            path.Activate();
            bodyMotion.Activate();
            editor.Activate();
            view.Activate();
        }
    }
}