using System;
using GravitySimulator.Gameplay.Orbital.Trajectory.Core;
using GravitySimulator.Gameplay.Orbital.Trajectory.Infrastructure;
using GravitySimulator.Infrastructure.Collections;
using GravitySimulator.Infrastructure.ComposableBehaviour;
using GravitySimulator.Math;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.Trajectory.Mechanics
{
    public class OrbitPath : ComposableChild<OrbitRoot>
    {
        public event Action<OrbitPathUpdateType> Updated;

        public CircularList<Vector2> Points => _points;
        public bool HasPath => _points.Count > 0; 

        [SerializeField] private int pointSpacing;

        private Orbit _orbit;
        private CircularList<Vector2> _points = new();

        public override void Initialize(OrbitRoot root)
        {
            base.Initialize(root);
        
            _orbit = Root.Orbit;
        }

        public override void Bind()
        {
            _orbit.Changed += RecalculatePath;            
        }

        private void OnDestroy()
        {
            _orbit.Changed -= RecalculatePath;    
        }

        private void RecalculatePath(OrbitChangeType orbitChangeType)
        {
            _points.Clear();
            if (pointSpacing <= 0) return;
            if (_orbit.HasAttractor == false) return;

            int pointCount = Mathf.CeilToInt(
                Geometry2D.Circumference(_orbit.Radius) / pointSpacing);

            float angleStep = 360f / pointCount;

            for (int i = 0; i < pointCount; ++i)
            {
                float angleRad = i * angleStep * Mathf.Deg2Rad;         
                Vector2 pathPoint = Geometry2D.PointOnCircle(
                    _orbit.AttractorCenter, 
                    _orbit.Radius,
                    angleRad
                );

                _points.Add(pathPoint);
            }

            OrbitPathUpdateType orbitPathUpdateType = orbitChangeType.ToPathUpdateType();
            Updated?.Invoke(orbitPathUpdateType);
        }
    }
}