using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Orbital
{
    [RequireComponent(typeof(OrbitComponent))]
    [RequireComponent(typeof(OrbitPath))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class OrbitBody : MonoBehaviour
    {
        public OrbitComponent OrbitalComponent { get; private set; }
        public OrbitPath OrbitPath { get; private set; }

        [SerializeField] private int pointCount;

        private Rigidbody2D _rb;

        private void Awake()
        {
            OrbitalComponent = GetComponent<OrbitComponent>();
            OrbitPath = GetComponent<OrbitPath>();

            _rb = GetComponent<Rigidbody2D>();
        
            OrbitPath.GenerateOrbitPoints(
                OrbitalComponent.AttractorCenter, 
                OrbitalComponent.OrbitRadius,
                pointCount
            );
        }
    }
}