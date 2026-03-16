using System;
using GravitySimulator.Interaction.Drag;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital
{
    public class OrbitBody : MonoBehaviour
    {
        public OrbitComponent OrbitComponent { get; private set; }

        public event Action OrbitChanged;

        public Vector2 AttractorCenter => attractor.position;
        public float OrbitRadius => orbitRadius;

        [Header("Orbit Settings")]
        [SerializeField] private Transform attractor;
        [SerializeField] private float orbitRadius;

        public void Initialize(OrbitComponent orbitComponent)
        {
            OrbitComponent = orbitComponent;
        }

        private void Start()
        {
            OrbitChanged?.Invoke();
        }

        public void RecalculateRadius()
        {
            orbitRadius = (OrbitComponent.Position - AttractorCenter).magnitude;
            OrbitChanged?.Invoke();
        }
    }
}