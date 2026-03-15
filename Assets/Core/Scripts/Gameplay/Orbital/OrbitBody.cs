using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Orbital
{
    [RequireComponent(typeof(OrbitComponent))]
    [RequireComponent(typeof(OrbitPath))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class OrbitBody : MonoBehaviour
    {
        public OrbitComponent OrbitalComponent => _orbitalComponent;
        public OrbitPath OrbitPath => _orbitPath;
        
        private OrbitComponent _orbitalComponent;
        private OrbitPath _orbitPath;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _orbitalComponent = GetComponent<OrbitComponent>();
            _rb = GetComponent<Rigidbody2D>();
        }
    }
}