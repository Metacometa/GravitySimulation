using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Gameplay.Orbital.Mechanics;
using System.Collections.Generic;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View
{
    public class OrbitPathPointsView : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }     
        
        [Header("Component References")]
        [SerializeField] private Transform viewRoot;

        [Header("Prefabs")]
        [SerializeField] private GameObject pointPrefab;

        private OrbitPath _path;

        private List<GameObject> _pointViews = new();

        private void OnDestroy()
        {
            _path.PathUpdated -= RebuildPoints;
        }

        public void Initialize(OrbitRoot root)
        {
            Root = root;
            
            _path = Root.Path;

            _path.PathUpdated += RebuildPoints;

            // SetVisible(false);
        }

        public void ShowOrbit() => SetVisible(true);

        public void HideOrbit() => SetVisible(false);

        private void SetVisible(bool state) => viewRoot.gameObject.SetActive(state);

        private void RebuildPoints()
        {
            ClearPoints();

            foreach (var point in _path.Points)
            {
                GameObject pointView = Instantiate(pointPrefab, point, Quaternion.identity, viewRoot);
                _pointViews.Add(pointView);
            }
        }
 
        private void ClearPoints()
        {
            foreach (var orbitPoint in _pointViews)
            {
                Destroy(orbitPoint);    
            }

            _pointViews.Clear();
        }
    }
}