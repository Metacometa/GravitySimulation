using GravitySimulator.Infrastructure.ComposableBehaviour;
using GravitySimulator.Gameplay.Attraction.Infrastructure;
using UnityEngine;
using Zenject;
using System.Numerics;

namespace GravitySimulator.Gameplay.Director
{
    public class AttractorSpawner : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float maxDistanceFromCenter;
        [SerializeField] private float minDistanceBetweenAttractors;

        [Header("Component References")]
        [SerializeField] private Transform starsParent;

        [Header("Prefabs")]
        [SerializeField] private GameObject starPrefab;

        [Inject] private DiContainer _container;
        [Inject] private AttractorRegistry _attractorRegistry;

        public void Spawn()
        {
            Vector2 spawnPosition = GeneratePosition();
            _container.InstantiatePrefab(starPrefab, spawnPosition, Quaternion.Identity, starsParent);
        }

        private Vector2 GeneratePosition()
        {
            Vector2 generatedPosition = Vector2.Zero;
            bool areRulesFollowed = false;

            while (!areRulesFollowed)
            {
                generatedPosition = GenerateRandomPosition();
                areRulesFollowed = CheckLocationRule(generatedPosition);
            }

            return generatedPosition;
        }
        
        private Vector2 GenerateRandomPosition()
        {
            return new Vector2(
                Random.Range(-maxDistanceFromCenter, maxDistanceFromCenter),
                Random.Range(-maxDistanceFromCenter, maxDistanceFromCenter)
            );
        }

        private bool CheckLocationRule(Vector2 position)
        {
            foreach (var attractor in _attractorRegistry.Items)
            {
                float distance = Vector2.Distance(position, attractor.transform.position);
                if (distance < minDistanceBetweenAttractors)
                    return false;
            }

            return true;
        }
    }
}
