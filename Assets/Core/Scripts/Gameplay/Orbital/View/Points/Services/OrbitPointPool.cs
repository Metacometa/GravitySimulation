using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Gameplay.Orbital.Interaction;
using GravitySimulator.Gameplay.Orbital.Mechanics;
using System.Collections.Generic;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View.Points
{
    public class OrbitPointPool : MonoBehaviour
    {
        public IReadOnlyList<GameObject> PointsViews => _pointViews;

        [Header("Component References")]
        [SerializeField] private Transform pointsPool;

        [Header("Prefabs")]
        [SerializeField] private GameObject pointPrefab;

        private readonly List<GameObject> _pointViews = new();
        
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
                    Vector2 point = targetPoints[i];
                    
                    GameObject pointView = Instantiate(pointPrefab, point, Quaternion.identity, pointsPool);
                    _pointViews.Add(pointView);
                }

                _poolSize = targetPoints.Count;
            }
            else if (_poolSize > targetPoints.Count)
            {
                for (int i = targetPoints.Count; i < _poolSize; ++i)
                {
                    _pointViews[i].SetActive(false);
                }

                _poolSize = targetPoints.Count;
            }
        }

        public void SetPosition(Vector2 position)
        {
            foreach (GameObject go in _pointViews)
                go.transform.position = position;
        }
    }
}