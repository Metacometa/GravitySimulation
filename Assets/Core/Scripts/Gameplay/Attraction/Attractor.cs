using GravitySimulator.Gameplay.Attraction.Infrastructure;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Attraction
{
    public class Attractor : MonoBehaviour
    {
        [Inject] private AttractorRegistry _attractorRegistry;

        private void Awake()
        {
            _attractorRegistry.Add(this);
        }

        private void OnDestroy()
        {
            _attractorRegistry.Remove(this);
        }
    }
}