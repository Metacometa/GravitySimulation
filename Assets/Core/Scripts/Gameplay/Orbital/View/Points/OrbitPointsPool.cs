using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Gameplay.Orbital.Interaction;
using GravitySimulator.Gameplay.Orbital.Mechanics;
using System.Collections.Generic;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View.Points
{
    public class OrbitPointsPool : MonoBehaviour
    {
        public IReadOnlyList<GameObject> Pool => _pool;

        [Header("Component References")]
        [SerializeField] private Transform pointsPool;

        [Header("Prefabs")]
        [SerializeField] private GameObject pointPrefab;

        private readonly List<GameObject> _pool = new();
        
        private int _poolSize = 0;

        public void ResizePool(IReadOnlyList<Vector2> _points)
        {
            if (_poolSize == _points.Count)
            {   
                return;
            }
            else if (_poolSize < _points.Count)
            {   
                for (int i = _poolSize; i < _points.Count; ++i)
                {
                    Vector2 point = _points[i];
                    
                    GameObject pointView = Instantiate(pointPrefab, point, Quaternion.identity, pointsPool);
                    _pool.Add(pointView);
                }

                _poolSize = _points.Count;
            }
            else if (_poolSize > _points.Count)
            {
                for (int i = _points.Count; i < _poolSize; ++i)
                {
                    _pool[i].SetActive(false);
                }

                _poolSize = _points.Count;
            }
        }

        public void SetPosition(Vector2 position)
        {
            foreach (GameObject go in _pool)
                go.transform.position = position;
        }
    }
}