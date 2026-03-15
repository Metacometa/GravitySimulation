using System;
using System.Collections.Generic;
using GravitySimulator.Math;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital
{
    [RequireComponent(typeof(OrbitComponent))]
    public class OrbitPath : MonoBehaviour
    {
        public event Action PathUpdated;

        public OrbitComponent OrbitComponent { get; private set; }
        public IReadOnlyList<Vector2> Path => _path;

        [SerializeField] private int pointCount;

        private List<Vector2> _path = new();

        private void Awake()
        {
            OrbitComponent = GetComponent<OrbitComponent>();

            OrbitComponent.OrbitChanged += RecalculatePath;
        }
    
        private void OnDestroy()
        {
            OrbitComponent.OrbitChanged -= RecalculatePath;    
        }

        private void RecalculatePath()
        {
            _path.Clear();

            if (pointCount <= 0)
                return;

            float angleStep = 360f / pointCount;

            for (int i = 0; i < pointCount; ++i)
            {
                float angleRad = i * angleStep * Mathf.Deg2Rad;         

                Vector2 pathPoint = Geometry2D.PointOnCircle(
                    OrbitComponent.AttractorCenter, 
                    OrbitComponent.OrbitRadius,
                    angleRad
                );

                _path.Add(pathPoint);
            }

            PathUpdated?.Invoke();
        }
    }
}