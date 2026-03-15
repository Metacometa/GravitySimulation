using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Orbital
{
    public class OrbitComponent : MonoBehaviour
    {
        public Vector2 AttractorCenter => attractor.position;
        public float OrbitRadius => orbitRadius;

        [SerializeField] private Transform attractor;
        [SerializeField] private float orbitRadius;
    }
}