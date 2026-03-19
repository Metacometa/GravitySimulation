using System;
using System.Collections.Generic;
using System.Threading;
using GravitySimulator.Math;
using Mono.Cecil.Cil;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital
{
    public class OrbitPath : MonoBehaviour
    {
        public event Action PathUpdated;

        public OrbitComponent OrbitComponent { get; private set; }
        public IReadOnlyList<Vector2> Path => _path;

        [SerializeField] private int pointSpacing;

        private OrbitBody _orbitBody;
        private List<Vector2> _path = new();

        private void OnDestroy()
        {
            _orbitBody.OrbitChanged -= RecalculatePath;    
        }

        public void Initialize(OrbitComponent orbitComponent)
        {
            OrbitComponent = orbitComponent;
        
            _orbitBody = OrbitComponent.Body;

            _orbitBody.OrbitChanged += RecalculatePath;
        }

        private void RecalculatePath()
        {
            _path.Clear();
            if (pointSpacing <= 0) return;

            int pointCount = Mathf.CeilToInt(
                Geometry2D.Circumference(_orbitBody.OrbitRadius) / pointSpacing);

            float angleStep = 360f / pointCount;

            for (int i = 0; i < pointCount; ++i)
            {
                float angleRad = i * angleStep * Mathf.Deg2Rad;         
                Vector2 pathPoint = Geometry2D.PointOnCircle(
                    _orbitBody.AttractorCenter, 
                    _orbitBody.OrbitRadius,
                    angleRad
                );

                _path.Add(pathPoint);
            }

            PathUpdated?.Invoke();
        }
    
        public bool TryGetIndexOfClosestPoint(Vector2 source, out int closestIndex)
        {
            closestIndex = -1;

            if (_path.Count == 0)
            {
                return false;    
            }    
            
            float closestDistance = Mathf.Infinity;
            for (int i = 0; i < _path.Count; ++i)
            {
                float distance = Vector2.Distance(_path[i], source);
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

            if (_path.Count == 0)
            {
                return false;    
            }    
            
            float closestDistance = Mathf.Infinity;
            foreach (Vector2 point in _path)
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
            if (index >= _path.Count) 
                return false;

            int nextIndex = (index + 1) % _path.Count;
            nextPoint = _path[nextIndex];

            return true;
        }

        public bool TryGetNextOrbitPointIndex(int index, out int nextIndex)
        {
            nextIndex = -1;
            if (index >= _path.Count) 
                return false;

            nextIndex = (index + 1) % _path.Count;

            return true;
        }        
    }
}