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

        private OrbitModel _model;
        private CircularList<Vector2> _points = new();

        public override void Initialize(OrbitRoot root)
        {
            base.Initialize(root);
        
            _model = Root.Orbit;
        }

        public override void Bind()
        {
            _model.Changed += RecalculatePath;            
        }

        private void OnDestroy()
        {
            _model.Changed -= RecalculatePath;    
        }

        private void RecalculatePath(OrbitChangeType orbitChangeType)
        {
            _points.Clear();
            if (pointSpacing <= 0) return;
            if (_model.HasAttractor == false) return;

            int pointCount = Mathf.CeilToInt(
                Geometry2D.Circumference(_model.Radius) / pointSpacing);

            float angleStep = 360f / pointCount;

            for (int i = 0; i < pointCount; ++i)
            {
                float angleRad = i * angleStep * Mathf.Deg2Rad;         
                Vector2 pathPoint = Geometry2D.PointOnCircle(
                    _model.AttractorCenter, 
                    _model.Radius,
                    angleRad
                );

                _points.Add(pathPoint);
            }

            OrbitPathUpdateType orbitPathUpdateType = orbitChangeType.ToPathUpdateType();
            Updated?.Invoke(orbitPathUpdateType);
        }
    }
}