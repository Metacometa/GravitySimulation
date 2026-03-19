using System;
using System.Collections.Generic;
using GravitySimulator.Math;
using Unity.VisualScripting;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital
{
    public class OrbitMotion : MonoBehaviour
    {
        public OrbitComponent OrbitComponent { get; private set; }

        [SerializeField] private float motionSpeed;
        [SerializeField] private float distanceToGetNextPoint;

        private OrbitPath _orbitPath;

        // private Vector2 _targetPoint;
        private int _targetPointIndex;

        public void Initialize(OrbitComponent orbitComponent)
        {
            OrbitComponent = orbitComponent;
        
            _orbitPath = OrbitComponent.Path;

            _orbitPath.PathUpdated += UpdateTargetPoint;

            UpdateTargetPoint();
        }

        private void OnDestroy()
        {
            _orbitPath.PathUpdated -= UpdateTargetPoint;
        }

        private void FixedUpdate()
        {
            float distance = Vector2.Distance(OrbitComponent.Position, _orbitPath.Path[_targetPointIndex]);
            
            if (distance < distanceToGetNextPoint &&
                _orbitPath.TryGetNextOrbitPointIndex(_targetPointIndex, out int nextIndex))
            {
                // OrbitComponent.Rigidbody.linearVelocity = Vector2.zero;
                _targetPointIndex = nextIndex;
            }

            Move(_orbitPath.Path[_targetPointIndex]);  
        }

        private void Move(Vector2 target)
        {
            Vector2 dir = (target - OrbitComponent.Position).normalized;

            OrbitComponent.Rigidbody.linearVelocity = dir * motionSpeed;

            // OrbitComponent.Rigidbody.AddForce(dir * motionSpeed);
        }
    
        private void UpdateTargetPoint()
        {
            if (_orbitPath.TryGetIndexOfClosestPoint(OrbitComponent.Position, out int closestIndex))
            {
                _targetPointIndex = closestIndex;
            }            
        }
    }
}