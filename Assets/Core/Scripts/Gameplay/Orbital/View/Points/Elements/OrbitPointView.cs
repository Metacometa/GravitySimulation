using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View.Points
{
    public class OrbitPointView : MonoBehaviour
    {
        public bool HasTargetReached => _transformTargets.Count == 0 && _vectorTargets.Count == 0;

        public Vector2 Position => transform.position;

        private const float DefaultEpsilon = 0.001f;

        private Queue<Transform> _transformTargets = new();
        private Queue<Vector2> _vectorTargets = new();
        // private Vector2 _target;

        public void AddTarget(Transform target)
        {
            _transformTargets.Enqueue(target);
        }

        public void AddTarget(Vector2 target)
        {
            _vectorTargets.Enqueue(target);
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void ClearTargets()
        {
            _transformTargets.Clear();            
            _vectorTargets.Clear();            
        }

        public void TickMove(float speed, float epsilon = DefaultEpsilon)
        {
            if (HasTargetReached)
                return;

            Vector2 target = _transformTargets.Count != 0 ? _transformTargets.Peek().position : _vectorTargets.Peek();
            transform.position = Vector2.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );

            if (HasReached(target, epsilon))
            {
                transform.position = target;
                if (_transformTargets.Count != 0)
                    _transformTargets.Dequeue();
                else
                    _vectorTargets.Dequeue();
            }
        }

        private bool HasReached(Vector2 target, float epsilon = DefaultEpsilon)
        {
            return Vector2.Distance(transform.position, target) <= epsilon;
        }
    }
}