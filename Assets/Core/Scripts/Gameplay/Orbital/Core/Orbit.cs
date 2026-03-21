using System;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Orbital.Core
{
    public class Orbit : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }

        public event Action OrbitChanged;

        public Vector2 AttractorCenter => attractor.transform.position;
        public float Radius => radius;

        [Header("Orbit Settings")]
        [SerializeField] private Attractor attractor;
        [SerializeField] private float radius;

        [Space]
        [SerializeField] private float radiusChangingSpeed;

        [Inject] private AttractorRegistry _attractorRegistry;

        public void Initialize(OrbitRoot root)
        {
            Root = root;
        }

        private void Start()
        {
            OrbitChanged?.Invoke();
        }

        public void ChangeRadius(float radiusDelta)
        {
            radius += radiusDelta * radiusChangingSpeed;
            OrbitChanged?.Invoke();
        }

        public void UpdateAttractor()
        {
            Attractor closestAttractor = null;
            float closestDistance = Mathf.Infinity;

            foreach (Attractor attractorItem in _attractorRegistry.Items)
            {
                float distance = ((Vector2)attractorItem.transform.position - Root.Position).magnitude;

                if (distance < closestDistance)
                {
                    closestAttractor = attractorItem;
                    closestDistance = distance;
                }
            }

            attractor = closestAttractor;

            OrbitChanged?.Invoke();
        }
    }
}