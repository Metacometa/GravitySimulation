using System;
using System.Collections.Generic;
using GravitySimulator.Math;
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

        private float _angleOffset = 0f;

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

            if (pointSpacing <= 0)
                return;

            int pointCount = Mathf.CeilToInt(
                Geometry2D.Circumference(_orbitBody.OrbitRadius) / pointSpacing);

            float angleStep = 360f / pointCount;
            _angleOffset = Mathf.Repeat(
                _angleOffset + 0.5f * Time.deltaTime,
                Geometry2D.OneFullTurn()
            );
            for (int i = 0; i < pointCount; ++i)
            {
                float angleRad = _angleOffset + i * angleStep * Mathf.Deg2Rad;         

                Vector2 pathPoint = Geometry2D.PointOnCircle(
                    _orbitBody.AttractorCenter, 
                    _orbitBody.OrbitRadius,
                    angleRad
                );

                _path.Add(pathPoint);
            }

            PathUpdated?.Invoke();
        }
    }
}