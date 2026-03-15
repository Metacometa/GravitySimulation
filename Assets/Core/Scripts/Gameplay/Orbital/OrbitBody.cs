using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Orbital
{
    [RequireComponent(typeof(OrbitComponent))]
    [RequireComponent(typeof(OrbitPath))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class OrbitBody : MonoBehaviour
    {
        public OrbitComponent OrbitComponent { get; private set; }
        public OrbitPath OrbitPath { get; private set; }

        private Rigidbody2D _rb;

        private void Awake()
        {
            OrbitComponent = GetComponent<OrbitComponent>();
            OrbitPath = GetComponent<OrbitPath>();

            _rb = GetComponent<Rigidbody2D>();
        }
    }
}