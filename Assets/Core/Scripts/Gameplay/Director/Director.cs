using GravitySimulator.Infrastructure.ComposableBehaviour;
using UnityEngine;

namespace GravitySimulator.Gameplay.Director
{
    public class Director : ComposableRoot
    {
        [Header("Settings")]
        [SerializeField] private float spawnStarInterval;

        [Header("Component References")]
        [SerializeField] private Spawner spawner;

        private void Start()
        {
            StartCoroutine(SpawnStar());
        }

        private IEnumerator SpawnStar()
        {
            while (true)
            {                
                spawner.SpawnStar();
                yield return new WaitForSeconds(spawnStarInterval); 
            }
        }
    }
}