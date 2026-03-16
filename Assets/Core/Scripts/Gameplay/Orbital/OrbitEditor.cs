using System.Collections.Generic;
using GravitySimulator.Interaction.Drag;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital
{
    public class OrbitEditor : MonoBehaviour
    {
        public OrbitComponent OrbitComponent { get; private set; }     
    
        [Header("Component References")]
        [SerializeField] private DragMove dragMove;

        private OrbitBody _orbitBody;

        public void Initialize(OrbitComponent orbitComponent)
        {
            OrbitComponent = orbitComponent;

            _orbitBody = OrbitComponent.Body;

            dragMove.Moved += EditOrbit;
        }

        private void OnDestroy()
        {
            dragMove.Moved -= EditOrbit;
        }

        private void EditOrbit()
        {
            _orbitBody.RecalculateRadius();
        }
    }
}