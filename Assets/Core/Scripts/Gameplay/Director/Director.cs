using GravitySimulator.Infrastructure.ComposableBehaviour;
using UnityEngine;

namespace GravitySimulator.Gameplay.Director
{
    public class Director : ComposableRoot
    {
        [SerializeField] private Spawner spawner;

        private void Update()
        {
            spawner.SpawnStar();
        }
    }
}