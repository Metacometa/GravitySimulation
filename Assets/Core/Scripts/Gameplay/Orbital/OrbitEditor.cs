using GravitySimulator.Interaction.Drag;
using GravitySimulator.Interaction.Input;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Orbital
{
    public class OrbitEditor : MonoBehaviour
    {
        public OrbitComponent OrbitComponent { get; private set; }     
    
        [Header("Component References")]
        [SerializeField] private Draggable draggable;

        [Inject] private InputReader _inputReader;

        private OrbitBody _orbitBody;
        private OrbitView _orbitView;

        public void Initialize(OrbitComponent orbitComponent)
        {
            OrbitComponent = orbitComponent;

            _orbitBody = OrbitComponent.Body;
            _orbitView = OrbitComponent.View;

            _inputReader.Scroll += EditOrbit;

            draggable.DragStart += ShowOrbitView;
            draggable.DragEnd += HideOrbitView;
            draggable.DragEnd += EditOrbit;
        }

        private void OnDestroy()
        {
            _inputReader.Scroll -= EditOrbit;

            draggable.DragStart -= ShowOrbitView;
            draggable.DragEnd -= HideOrbitView;
            draggable.DragEnd -= EditOrbit;
        }

        private void EditOrbit(float radiusDelta)
        {
            _orbitBody.ChangeRadius(radiusDelta);
        }

        private void EditOrbit()
        {
            _orbitBody.ChangeRadius(0f);
        }

        private void ShowOrbitView()
        {
            _orbitView.DisplayOrbit(true);
        }

        private void HideOrbitView()
        {
            _orbitView.DisplayOrbit(false);
        }
    }
}