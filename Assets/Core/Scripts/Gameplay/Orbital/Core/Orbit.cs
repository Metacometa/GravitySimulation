using System;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Orbital.Core
{
    public class Orbit : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }

        public event Action RadiusChanged;
        public event Action AttractorChanged;

        public bool HasAttractor => attractor != null;
        public Vector2 AttractorCenter => attractor != null ? attractor.transform.position : transform.position;
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

        public void InitializeActions()
        {
            Root.Editor.RadiusEdited += ChangeRadius;
            Root.Editor.PositionEdited += UpdateAttractor;
        }

        private void Start()
        {
            RadiusChanged?.Invoke();
        }

        private void OnDestroy()
        {
            Root.Editor.RadiusEdited -= ChangeRadius;     
            Root.Editor.PositionEdited -= UpdateAttractor;       
        }

        private void ChangeRadius(float radiusDelta)
        {
            radius += radiusDelta * radiusChangingSpeed;
            RadiusChanged?.Invoke();
        }

        private void UpdateAttractor()
        {
            Attractor previousAttractor = attractor;

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

            if (attractor == closestAttractor)
                return;

            attractor = closestAttractor;
            AttractorChanged?.Invoke();
        }
    }
}