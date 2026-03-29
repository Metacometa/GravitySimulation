using GravitySimulator.Infrastructure.ComposableBehaviour;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Director
{
    public class Spawner : MonoBehaviour
    {
        [Header("Component References")]
        [SerializeField] private Transform starsParent;

        [Header("Prefabs")]
        [SerializeField] private GameObject starPrefab;

        [Inject] private DiContainer _container;

        public void SpawnStar()
        {
            _container.InstantiatePrefab(starPrefab, starsParent);
        }
    }
}
