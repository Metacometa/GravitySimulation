using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Gameplay.Orbital.Interaction;
using GravitySimulator.Gameplay.Orbital.Mechanics;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
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

        public void Initialize(OrbitRoot root)
        {
            Root = root;
            
            _orbit = Root.Orbit;
            _path = Root.Path;
            _edit = Root.Editor;
        }

        public void InitializeActions()
        {
            _edit.EditStarted += ShowOrbit;

            _orbit.RadiusChanged += RebuildPoints;
            _orbit.AttractorChanged += MovePoints;
    
            _edit.EditEnded += HideOrbit;   
        }  

        private void OnDestroy()
        {
            _edit.EditStarted -= ShowOrbit;

            _orbit.RadiusChanged -= RebuildPoints;
            _orbit.AttractorChanged -= MovePoints;

            _edit.EditEnded -= HideOrbit; 
        }

        public void ShowOrbit()
        {
            if (_orbit.AttractorCenter != null)
                orbitPointsAnimator.AnimateEditingStart(Root.Orbit.AttractorCenter, _path.Points.Items);
        }

        public void HideOrbit()
        {
            if (_orbit.AttractorCenter != null)
                orbitPointsAnimator.AnimateEditingEnd(Root.Orbit.AttractorCenter);            
        }

        private void MovePoints()
        {
            if (_orbit.AttractorCenter != null)
                orbitPointsAnimator.AnimateCenterChangedEditing(_path.Points.Items);
        }

        private void RebuildPoints()
        {
            if (_orbit.AttractorCenter != null)
                orbitPointsAnimator.AnimateRadiusEditing(_path.Points.Items);            
        }
    }
}