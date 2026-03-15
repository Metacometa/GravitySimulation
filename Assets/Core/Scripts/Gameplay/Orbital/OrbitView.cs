using System.Collections.Generic;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital
{
    [RequireComponent(typeof(OrbitPath))]
    public class OrbitView : MonoBehaviour
    {
        public OrbitPath OrbitPath { get; private set; }     
        
        [Header("Component References")]
        [SerializeField] private Transform orbitViewRoot;

        [Header("Prefabs")]
        [SerializeField] private GameObject orbitPointViewPrefab;

        private List<GameObject> _orbitPointViews = new();

        private void Awake()
        {
            OrbitPath = GetComponent<OrbitPath>();

            OrbitPath.PathUpdated += UpdateOrbitView;
        }

        private void OnDestroy()
        {
            OrbitPath.PathUpdated -= UpdateOrbitView;
        }

        private void UpdateOrbitView()
        {
            ClearOrbitView();

            foreach (var point in OrbitPath.Path)
            {
                GameObject pointView = Instantiate(orbitPointViewPrefab, point, Quaternion.identity, orbitViewRoot);
                _orbitPointViews.Add(pointView);
            }
        }
 
        private void ClearOrbitView()
        {
            foreach (var orbitPoint in _orbitPointViews)
            {
                Destroy(orbitPoint);    
            }

            _orbitPointViews.Clear();
        }
    }
}