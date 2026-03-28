using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Gameplay.Orbital.Interaction;
using GravitySimulator.Gameplay.Orbital.Mechanics;
using GravitySimulator.Infrastructure.ComposableBehaviour;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View.Points
{
    public class OrbitPointsView : ComposableChild<OrbitRoot>
    {        
        [Header("Component References")]
        [SerializeField] private OrbitPointsAnimator orbitPointsAnimator;

        private Orbit _orbit;
        private OrbitPath _path;
        private OrbitEditor _edit;

        public override void Initialize(OrbitRoot root)
        {
            base.Initialize(root);
            
            _orbit = Root.Orbit;
            _path = Root.Path;
            _edit = Root.Editor;
        }

        public override void Bind()
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
                orbitPointsAnimator.AnimateEditingStart(Root.Orbit.AttractorCenter, _path.Points);
        }

        public void HideOrbit()
        {
            if (_orbit.AttractorCenter != null)
                orbitPointsAnimator.AnimateEditingEnd(Root.Orbit.AttractorCenter);            
        }

        private void MovePoints()
        {
            if (_edit.IsEditing == true &&
                _orbit.AttractorCenter != null)
                orbitPointsAnimator.AnimateCenterChangedEditing(Root.Orbit.AttractorCenter, _path.Points);
        }

        private void RebuildPoints()
        {
            if (_edit.IsEditing == true &&
                _orbit.AttractorCenter != null)
                orbitPointsAnimator.AnimateRadiusEditing(_path.Points.Items);            
        }
    }
}