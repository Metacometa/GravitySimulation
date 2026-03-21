using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Gameplay.Orbital.View;
using GravitySimulator.Interaction.Input;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Orbital.Interaction
{
    public class OrbitEditor : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }     
    
        [Header("Component References")]
        [SerializeField] private Draggable draggable;

        [Inject] private InputReader _inputReader;

        private Orbit _orbit;
        private OrbitPathView _pathView;

        public void Initialize(OrbitRoot root)
        {
            Root = root;

            _orbit = Root.Orbit;
            _pathView = Root.View.PathView;

            _inputReader.Scroll += EditOrbit;

            draggable.DragStart += _pathView.ShowOrbit;

            draggable.Drag += _orbit.UpdateAttractor;

            draggable.DragEnd += _orbit.UpdateAttractor;
            draggable.DragEnd += _pathView.HideOrbit;
        }

        private void OnDestroy()
        {
            _inputReader.Scroll -= EditOrbit;

            draggable.DragStart -= _pathView.ShowOrbit;

            draggable.Drag -= _orbit.UpdateAttractor;

            draggable.DragEnd -= _orbit.UpdateAttractor;
            draggable.DragEnd -= _pathView.HideOrbit;
        }

        private void EditOrbit(float radiusDelta)
        {
            if (draggable.IsDragging)
                _orbit.ChangeRadius(radiusDelta);
        }
    }
}