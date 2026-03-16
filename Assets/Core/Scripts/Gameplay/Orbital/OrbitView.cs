using System.Collections.Generic;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital
{
    public class OrbitView : MonoBehaviour
    {
        public OrbitComponent OrbitComponent { get; private set; }     
        
        [Header("Component References")]
        [SerializeField] private Transform orbitViewRoot;

        [Header("Prefabs")]
        [SerializeField] private GameObject orbitPointViewPrefab;

        private OrbitPath _orbitPath;

        private List<GameObject> _orbitPointViews = new();

        private void OnDestroy()
        {
            _orbitPath.PathUpdated -= UpdateOrbitView;
        }

        public void Initialize(OrbitComponent orbitComponent)
        {
            OrbitComponent = orbitComponent;
            
            _orbitPath = OrbitComponent.Path;

            _orbitPath.PathUpdated += UpdateOrbitView;
        }
        
        private void UpdateOrbitView()
        {
            ClearOrbitView();

            foreach (var point in _orbitPath.Path)
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