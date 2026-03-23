using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Gameplay.Orbital.Interaction;
using GravitySimulator.Gameplay.Orbital.Mechanics;
using System.Collections.Generic;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View.Points
{
    public class OrbitPointsView : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }     
        
        [Header("Component References")]
        [SerializeField] private OrbitPointsAnimator orbitPointsAnimator;

        private Orbit _orbit;
        private OrbitPath _path;
        private OrbitEditor _edit;

        private List<GameObject> _pointViews = new();

        public void Initialize(OrbitRoot root)
        {
            Root = root;
            
            _orbit = Root.Orbit;
            _path = Root.Path;
            _edit = Root.Editor;
        }

        public void InitializeActions()
        {
            _path.PathUpdated += RebuildPoints;

            _edit.EditStarted += ShowOrbit;
            _edit.EditEnded += HideOrbit;   
        }  

        private void OnDestroy()
        {
            _path.PathUpdated -= RebuildPoints;

            _edit.EditStarted -= ShowOrbit;
            _edit.EditEnded -= HideOrbit; 
        }

        public void ShowOrbit()
        {
            if (_orbit.AttractorCenter != null)
                orbitPointsAnimator.AnimateEditingStart(Root.Orbit.AttractorCenter, _path.Points);
        }

        public void HideOrbit()
        {
            if (_orbit.AttractorCenter != null)
                orbitPointsAnimator.AnimateEditingEnd(Root.Orbit.AttractorCenter);            
        }

        private void RebuildPoints()
        {
            ClearPoints();

            // foreach (var point in _path.Points)
            // {
            //     GameObject pointView = Instantiate(pointPrefab, point, Quaternion.identity, viewRoot);
            //     _pointViews.Add(pointView);
            // }
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