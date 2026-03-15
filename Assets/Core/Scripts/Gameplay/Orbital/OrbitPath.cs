using System;
using System.Collections.Generic;
using GravitySimulator.Math;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital
{
    public class OrbitPath : MonoBehaviour
    {
        public List<Vector2> PathPoints => _pathPoints;

        public OrbitComponent OrbitalComponent => _orbitalComponent;
        
        
        private OrbitComponent _orbitalComponent;

        private List<Vector2> _pathPoints;

        private void Awake()
        {
            _orbitalComponent = GetComponent<OrbitComponent>();
        }

        public void GenerateOrbitPoints(Vector2 center, float radius, int numberOfPoints)
        {
            _pathPoints.Clear();

            if (numberOfPoints <= 0)
                return;

            float angleStep = 360f / numberOfPoints;

            for (int i = 0; i < numberOfPoints; ++i)
            {
                float angleRad = i * angleStep * Mathf.Deg2Rad;         
                _pathPoints.Add(Geometry2D.PointOnCircle(center, angleRad, radius));
            }
        }
    }
}