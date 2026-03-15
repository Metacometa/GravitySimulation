using System;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Orbital
{
    public class OrbitBody : MonoBehaviour
    {
        public event Action OrbitChanged;

        public OrbitComponent OrbitComponent { get; private set; }

        public Vector2 AttractorCenter => attractor.position;
        public float OrbitRadius => orbitRadius;

        [Header("Orbit Settings")]
        [SerializeField] private Transform attractor;
        [SerializeField] private float orbitRadius;

        private Rigidbody2D _rb;

        public void Initialize(OrbitComponent orbitComponent)
        {
            OrbitComponent = orbitComponent;
            _rb = OrbitComponent.Rb;
        }

        private void Start()
        {
            OrbitChanged?.Invoke();
        }

        public void UpdateOrbit()
        {
            orbitRadius = ((Vector2)OrbitComponent.transform.position - AttractorCenter).magnitude;
            OrbitChanged?.Invoke();
        }
    }
}