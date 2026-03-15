using System;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital
{
    public class OrbitComponent : MonoBehaviour
    {
        public event Action OrbitChanged;
    
        public Vector2 AttractorCenter => attractor.position;
        public float OrbitRadius => orbitRadius;

        [SerializeField] private Transform attractor;
        [SerializeField] private float orbitRadius;
    }
}