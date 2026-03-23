using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View.Points
{
    public class OrbitPointView : MonoBehaviour
    {
        public bool HasTargetReached { get; private set; }

        private const float DefaultEpsilon = 0.001f;

        private Vector2 _target;

        public void SetTarget(Vector2 target)
        {
            _target = target;
            HasTargetReached = false;

            CheckTargetReaching(DefaultEpsilon);
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void TickMove(float speed, float epsilon = DefaultEpsilon)
        {
            if (HasTargetReached)
                return;

            transform.position = Vector2.MoveTowards(
                transform.position,
                _target,
                speed * Time.deltaTime
            );

            CheckTargetReaching(epsilon);
        }

        private void CheckTargetReaching(float epsilon)
        {
            HasTargetReached = Vector2.Distance(transform.position, _target) <= epsilon;
            
            if (HasTargetReached)
                transform.position = _target;
        }
    }
}