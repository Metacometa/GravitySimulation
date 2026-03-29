using System;
using GravitySimulator.Gameplay.Attraction;
using GravitySimulator.Gameplay.Attraction.Infrastructure;
using GravitySimulator.Gameplay.Orbital.Trajectory.Infrastructure;
using GravitySimulator.Infrastructure.ComposableBehaviour;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Orbital.Trajectory.Core
{
    public class Orbit : ComposableChild<OrbitRoot>
    {
        public event Action<OrbitChangeType> Changed;

        public bool HasAttractor => attractor != null;
        public Vector2 AttractorCenter => attractor != null ? attractor.transform.position : transform.position;
        public float Radius => radius;

        [Header("Orbit Settings")]
        [SerializeField] private Attractor attractor;
        [SerializeField] private float radius;

        [Space]
        [SerializeField] private float radiusChangingSpeed;

        [Inject] private AttractorRegistry _attractorRegistry;

        public override void Bind()
        {
            Root.Editor.RadiusEdited += ChangeRadius;
            Root.Editor.PositionEdited += UpdateAttractor;
        }

        public override void Activate()
        {
            UpdateAttractor();
        }

        private void Start()
        {
            Changed?.Invoke(OrbitChangeType.Radius);
        }

        private void OnDestroy()
        {
            Root.Editor.RadiusEdited -= ChangeRadius;     
            Root.Editor.PositionEdited -= UpdateAttractor;       
        }

        private void ChangeRadius(float radiusDelta)
        {
            radius += radiusDelta * radiusChangingSpeed;

            Changed?.Invoke(OrbitChangeType.Radius);
        }

        private void UpdateAttractor()
        {
            Attractor previousAttractor = attractor;

            Attractor closestAttractor = null;
            float closestDistance = Mathf.Infinity;

            foreach (Attractor attractorItem in _attractorRegistry.Items)
            {
                float distance = ((Vector2)attractorItem.transform.position - Root.BodyPosition).magnitude;

                if (distance < closestDistance)
                {
                    closestAttractor = attractorItem;
                    closestDistance = distance;
                }
            }

            if (attractor == closestAttractor)
                return;

            attractor = closestAttractor;

            Changed?.Invoke(OrbitChangeType.Attractor);
        }
    }
}