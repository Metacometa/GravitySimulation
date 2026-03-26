using System;
using System.Collections.Generic;
using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Infrastructure.Collections;
using GravitySimulator.Math;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.Mechanics
{
    public class OrbitPath : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }

        public event Action PathUpdated;

        public CircularList<Vector2> Points => _points;
        public bool HasPath => _points.Count > 0; 

        [SerializeField] private int pointSpacing;

        private Orbit _orbit;
        private CircularList<Vector2> _points = new();

        public void Initialize(OrbitRoot root)
        {
            Root = root;
        
            _orbit = Root.Orbit;
        }

        public void InitializeActions()
        {
            _orbit.RadiusChanged += RecalculatePath;            
            _orbit.AttractorChanged += RecalculatePath;       
        }

        private void OnDestroy()
        {
            _orbit.RadiusChanged -= RecalculatePath;    
            _orbit.AttractorChanged -= RecalculatePath;     
        }

        private void RecalculatePath()
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

            PathUpdated?.Invoke();
        }
    
        public bool TryGetIndexOfClosestPoint(Vector2 source, out int closestIndex)
        {
            closestIndex = -1;

            if (_points.Count == 0)
            {
                return false;    
            }    
            
            float closestDistance = Mathf.Infinity;
            for (int i = 0; i < _points.Count; ++i)
            {
                float distance = Vector2.Distance(_points[i], source);
                if (distance < closestDistance)
                {
                    closestIndex = i;
                    closestDistance = distance;
                }
            }

            return true;
        } 
    }
}