using System.Collections.Generic;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital
{
    [RequireComponent(typeof(OrbitComponent))]
    [RequireComponent(typeof(OrbitPath))]
    public class OrbitView : MonoBehaviour
    {
        public OrbitComponent OrbitComponent { get; private set; }   
        public OrbitPath OrbitPath { get; private set; }     
        
        [Header("Component References")]
        [SerializeField] private Transform orbitParent;

        [Header("Prefabs")]
        [SerializeField] private GameObject orbitPointPrefab;

        private List<GameObject> _orbitView = new();

        private void Awake()
        {
            OrbitComponent = GetComponent<OrbitComponent>();
            OrbitPath = GetComponent<OrbitPath>();
        }

        private void Start()
        {
            UpdateOrbit();
        }

        public void UpdateOrbit()
        {
            ResetOrbit();

            foreach (var point in OrbitPath.PathPoints)
            {
                Debug.Log($"[OrbitView] [UpdateOrbit]: point");
                GameObject pointView = Instantiate(orbitPointPrefab, point, Quaternion.identity, orbitParent);
                _orbitView.Add(pointView);
            }
        }
 
        private void ResetOrbit()
        {
            foreach (var orbitPoint in _orbitView)
            {
                Destroy(orbitPoint);    
            }

            _orbitView.Clear();
        }
    }
}