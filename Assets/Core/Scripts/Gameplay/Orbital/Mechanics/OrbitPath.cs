using System;
using System.Collections.Generic;
using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Math;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.Mechanics
{
    public class OrbitPath : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }

        public event Action PathUpdated;

        public IReadOnlyList<Vector2> Points => _points;
        public bool HasPath => _points.Count > 0; 

        [SerializeField] private int pointSpacing;

        private Orbit _orbit;
        private List<Vector2> _points = new();

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

        public bool TryGetClosestPoint(Vector2 source, out Vector2 closestPoint)
        {
            closestPoint = Vector2.zero;

            if (_points.Count == 0)
            {
                return false;    
            }    
            
            float closestDistance = Mathf.Infinity;
            foreach (Vector2 point in _points)
            {
                float distance = Vector2.Distance(point, source);
                if (distance < closestDistance)
                {
                    closestPoint = point;
                    closestDistance = distance;
                }
            }

            return true;
        }
    
        public bool TryGetNextOrbitPoint(int index, out Vector2 nextPoint)
        {
            nextPoint = Vector2.zero;
            if (index >= _points.Count) 
                return false;

            int nextIndex = (index + 1) % _points.Count;
            nextPoint = _points[nextIndex];

            return true;
        }

        public bool TryGetNextOrbitPointIndex(int index, out int nextIndex)
        {
            nextIndex = -1;
            if (index >= _points.Count) 
                return false;

            nextIndex = (index + 1) % _points.Count;

            return true;
        }        
    }
}