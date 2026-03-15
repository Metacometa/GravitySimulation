using System.Collections.Generic;
using GravitySimulator.Math;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital
{
    public class OrbitPath : MonoBehaviour
    {
        public IReadOnlyList<Vector2> PathPoints => _pathPoints;

        private List<Vector2> _pathPoints = new();

        public void GenerateOrbitPoints(Vector2 center, float radius, int pointCount)
        {
            _pathPoints.Clear();

            if (pointCount <= 0)
                return;

            float angleStep = 360f / pointCount;

            for (int i = 0; i < pointCount; ++i)
            {
                float angleRad = i * angleStep * Mathf.Deg2Rad;         
                _pathPoints.Add(Geometry2D.PointOnCircle(center, angleRad, radius));
            }
        }
    }
}