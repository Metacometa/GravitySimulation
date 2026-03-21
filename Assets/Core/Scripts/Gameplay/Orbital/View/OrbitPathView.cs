using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Gameplay.Orbital.Mechanics;
using System.Collections.Generic;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View
{
    public class OrbitPathView : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }     
        
        [Header("Component References")]
        [SerializeField] private Transform viewRoot;

        [Header("Prefabs")]
        [SerializeField] private GameObject pathPointPrefab;

        private OrbitPath _path;

        private List<GameObject> _pathPointViews = new();

        private void OnDestroy()
        {
            _path.PathUpdated -= UpdateOrbitView;
        }

        public void Initialize(OrbitRoot root)
        {
            Root = root;
            
            _path = Root.Path;

            _path.PathUpdated += UpdateOrbitView;

            DisplayOrbit(false);
        }

        public void ShowOrbit() => DisplayOrbit(true);

        public void HideOrbit() => DisplayOrbit(false);

        private void DisplayOrbit(bool state) => viewRoot.gameObject.SetActive(state);

        private void UpdateOrbitView()
        {
            ClearOrbitView();

            foreach (var point in _path.Path)
            {
                GameObject pointView = Instantiate(pathPointPrefab, point, Quaternion.identity, viewRoot);
                _pathPointViews.Add(pointView);
            }
        }
 
        private void ClearOrbitView()
        {
            foreach (var orbitPoint in _pathPointViews)
            {
                Destroy(orbitPoint);    
            }

            _pathPointViews.Clear();
        }
    }
}