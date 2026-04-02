using System.Collections;
using GravitySimulator.Infrastructure.ComposableBehaviour;
using UnityEngine;

namespace GravitySimulator.Gameplay.Director
{
    public class Director : ComposableRoot
    {
        [Header("Settings")]
        [SerializeField] private float spawnInterval;

        [Header("Component References")]
        [SerializeField] private AttractorSpawner spawner;

        [Inject] private ClusterRegistry _clusterRegistry;

        private void Start()
        {
            StartCoroutine(SpawnAttractor());
        }

        private IEnumerator SpawnAttractor()
        {
            while (true)
            {                
                spawner.Spawn();
                yield return new WaitForSeconds(spawnInterval); 
            }
        }
    }
}