using System.Collections.Generic;
using GravitySimulator.Infrastructure.Collections;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.Trajectory.View.Points
{
    public class OrbitPointPool : MonoBehaviour
    {
        public CircularList<OrbitPointView> PointsViews => _pointViews;

        [Header("Component References")]
        [SerializeField] private Transform pointsPool;

        [Header("Prefabs")]
        [SerializeField] private OrbitPointView pointPrefab;

        private readonly CircularList<OrbitPointView> _pointViews = new();
        
        private int _poolSize = 0;

        public void ResizePool(IReadOnlyList<Vector2> targetPoints)
        {
            if (_poolSize == targetPoints.Count)
            {   
                return;
            }
            else if (_poolSize < targetPoints.Count)
            {   
                for (int i = _poolSize; i < targetPoints.Count; ++i)
                {
                    Vector2 point = i == 0 ? targetPoints[i] : _pointViews[i - 1].Position;
                    
                    OrbitPointView pointView = Instantiate(pointPrefab, point, Quaternion.identity, pointsPool);
                    _pointViews.Add(pointView);
                }

                _poolSize = targetPoints.Count;
            }
            else if (_poolSize > targetPoints.Count)
            {
                for (int i = targetPoints.Count; i < _poolSize; ++i)
                {
                    OrbitPointView orbitPointView = _pointViews[i];

                    _pointViews.Remove(orbitPointView);
                    Destroy(orbitPointView.gameObject);
                }

                _poolSize = targetPoints.Count;
            }
        }

        public void SetPosition(Vector2 position)
        {
            foreach (OrbitPointView pointView in _pointViews.Items)
                pointView.SetPosition(position);
        }
    }
}