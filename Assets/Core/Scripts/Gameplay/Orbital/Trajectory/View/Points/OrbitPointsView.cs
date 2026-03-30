using GravitySimulator.Gameplay.Orbital.Trajectory.Core;
using GravitySimulator.Gameplay.Orbital.Trajectory.Infrastructure;
using GravitySimulator.Gameplay.Orbital.Trajectory.Interaction;
using GravitySimulator.Gameplay.Orbital.Trajectory.Mechanics;
using GravitySimulator.Infrastructure.ComposableBehaviour;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.Trajectory.View.Points
{
    public class OrbitPointsView : ComposableChild<OrbitRoot>
    {        
        [Header("Component References")]
        [SerializeField] private OrbitPointsAnimator orbitPointsAnimator;

        private OrbitModel _model;
        private OrbitPath _path;
        private OrbitEditor _edit;

        public override void Initialize(OrbitRoot root)
        {
            base.Initialize(root);
            
            _model = Root.Orbit;
            _path = Root.Path;
            _edit = Root.Editor;
        }

        public override void Bind()
        {
            _edit.EditStarted += ShowOrbit;

            _path.Updated += RebuildPoints;
            _path.Updated += MovePoints;
    
            _edit.EditEnded += HideOrbit;   
        }  

        private void OnDestroy()
        {
            _edit.EditStarted -= ShowOrbit;

            _path.Updated -= RebuildPoints;
            _path.Updated -= MovePoints;

            _edit.EditEnded -= HideOrbit; 
        }

        public void ShowOrbit()
        {
            if (_model.AttractorCenter != null)
                orbitPointsAnimator.AnimateEditingStart(Root.Orbit.AttractorCenter, _path.Points);
        }

        public void HideOrbit()
        {
            if (_model.AttractorCenter != null)
                orbitPointsAnimator.AnimateEditingEnd(Root.Orbit.AttractorCenter);            
        }

        private void MovePoints(OrbitPathUpdateType orbitPathUpdateType)
        {
            if (orbitPathUpdateType != OrbitPathUpdateType.Attractor) return;

            if (_edit.IsEditing == true &&
                _model.AttractorCenter != null)
                orbitPointsAnimator.AnimateCenterChangedEditing(Root.Orbit.AttractorCenter, _path.Points);
        }

        private void RebuildPoints(OrbitPathUpdateType orbitPathUpdateType)
        {
            if (orbitPathUpdateType != OrbitPathUpdateType.Radius) return;

            if (_edit.IsEditing == true &&
                _model.AttractorCenter != null)
                orbitPointsAnimator.AnimateRadiusEditing(_path.Points);            
        }
    }
}