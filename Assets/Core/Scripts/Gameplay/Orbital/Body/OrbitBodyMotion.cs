using GravitySimulator.Gameplay.Orbital.Trajectory.Core;
using GravitySimulator.Gameplay.Orbital.Trajectory.Infrastructure;
using GravitySimulator.Gameplay.Orbital.Trajectory.Mechanics;
using GravitySimulator.Infrastructure.ComposableBehaviour;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.Body.Mechanics
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class OrbitBodyMotion : ComposableChild<OrbitRoot>
    {
        [SerializeField] private float motionSpeed;
        [SerializeField] private float distanceToGetNextPoint;

        private OrbitPath _path;
        private int _targetPointIndex;
        private Rigidbody2D _rb;

        public override void Initialize(OrbitRoot root)
        {
            base.Initialize(root);
        
            _path = Root.Path;

            _rb = GetComponent<Rigidbody2D>();
        }

        public override void Bind()
        {
            _path.Updated += UpdateTargetPoint;
        }

        public override void Activate()
        {
            UpdateTargetPoint(OrbitPathUpdateType.Attractor);
        }

        private void OnDestroy()
        {
            _path.Updated -= UpdateTargetPoint;
        }

        private void FixedUpdate()
        {
            if (_path == null || 
                _path.HasPath == false) 
                return;

            float distance = Vector2.Distance(Root.BodyPosition, _path.Points[_targetPointIndex]);
            
            if (distance < distanceToGetNextPoint)
            {
                _targetPointIndex = _path.Points.GetNextIndex(_targetPointIndex);
            }

            Move(_path.Points[_targetPointIndex]);  
        }

        private void Move(Vector2 target)
        {
            Vector2 dir = (target - Root.BodyPosition).normalized;

            _rb.linearVelocity = dir * motionSpeed;
        }
    
        private void UpdateTargetPoint(OrbitPathUpdateType orbitPathUpdateType)
        {
            if (Math.Geometry2D.TryGetClosestIndex(_path.Points.Items, Root.BodyPosition, out int closestIndex))
            {
                _targetPointIndex = closestIndex;
            }            
        }
    }
}