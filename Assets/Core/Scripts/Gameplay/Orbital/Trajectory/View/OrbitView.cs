using GravitySimulator.Gameplay.Orbital.Trajectory.Core;
using GravitySimulator.Gameplay.Orbital.Trajectory.View.Line;
using GravitySimulator.Gameplay.Orbital.Trajectory.View.Points;
using GravitySimulator.Infrastructure.ComposableBehaviour;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.Trajectory.View
{
    public class OrbitView : ComposableChild<OrbitRoot>
    {
        [Header("Component References")]
        [SerializeField] private OrbitPointsView pointsView;
        [SerializeField] private OrbitPathLineView pathLineView;

        public override void Initialize(OrbitRoot root)
        {
            base.Initialize(root);

            pointsView.Initialize(Root);
            pathLineView.Initialize(Root);
        }

        public override void Bind()
        {
            pointsView.Bind();
            pathLineView.Bind();
        }

        public override void Activate()
        {
            pointsView.Activate();
            pathLineView.Activate();
        }
    }
}