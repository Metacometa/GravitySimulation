using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Orbital
{
    public class OrbitComponent : MonoBehaviour
    {
        [SerializeField] private Transform attractor;
    }
}