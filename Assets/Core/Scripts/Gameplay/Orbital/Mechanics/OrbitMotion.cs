using GravitySimulator.Gameplay.Orbital.Core;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.Mechanics
{
    public class OrbitMotion : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }

        [SerializeField] private float motionSpeed;
        [SerializeField] private float distanceToGetNextPoint;

        private OrbitPath _path;

        // private Vector2 _targetPoint;
        private int _targetPointIndex;

        public void Initialize(OrbitRoot root)
        {
            Root = root;
        
            _path = Root.Path;

            _path.PathUpdated += UpdateTargetPoint;

            UpdateTargetPoint();
        }

        private void OnDestroy()
        {
            _path.PathUpdated -= UpdateTargetPoint;
        }

        private void FixedUpdate()
        {
            if (!_path.HasPath) return;

            float distance = Vector2.Distance(Root.Position, _path.Path[_targetPointIndex]);
            
            if (distance < distanceToGetNextPoint &&
                _path.TryGetNextOrbitPointIndex(_targetPointIndex, out int nextIndex))
            {
                // OrbitComponent.Rigidbody.linearVelocity = Vector2.zero;
                _targetPointIndex = nextIndex;
            }

            Move(_path.Path[_targetPointIndex]);  
        }

        private void Move(Vector2 target)
        {
            Vector2 dir = (target - Root.Position).normalized;

            Root.Rigidbody.linearVelocity = dir * motionSpeed;

            // OrbitComponent.Rigidbody.AddForce(dir * motionSpeed);
        }
    
        private void UpdateTargetPoint()
        {
            if (_path.TryGetIndexOfClosestPoint(Root.Position, out int closestIndex))
            {
                _targetPointIndex = closestIndex;
            }            
        }
    }
}