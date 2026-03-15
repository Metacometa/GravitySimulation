using GravitySimulator.Gameplay.Gravity.Infrastructure;
using GravitySimulator.Gameplay.Gravity.Config;
using GravitySimulator.Gameplay.Gravity.Mechanics;
using GravitySimulator.Physics;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Gravity.Simulation
{
    [RequireComponent(typeof(GravityComponent))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class GravityBody : MonoBehaviour
    {
        public GravityComponent GravityComponent { get; private set; }

        [Header("Motion")]
        [SerializeField] private float startSpeed;
        [SerializeField] private float maxVelocity;

        [Header("Orbit Stabilizing")]
        [SerializeField, Range(0f, 10f)]
        private float orbitStabilizingFactor;

        [Inject] private GravityBodyRegistry _gravityBodyRegistry;
        [Inject] private GravityConfig _gravityConfig;

        private Rigidbody2D _rb;

        private Vector2 _accumulatedForce;
        private GravityComponent _strongestAttractor;
        private float _strongestGravitationalForce;

        private void Awake()
        {
            GravityComponent = GetComponent<GravityComponent>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _gravityBodyRegistry.GravityBodies.Add(this);
        }

        private void OnDisable()
        {
            _gravityBodyRegistry.GravityBodies.Remove(this);
        }

        public void AddGravitationalForce(GravityBody other)
        {
            Vector2 gravitationalForce = GravityPhysics.GravitationalForceOn(
                _gravityConfig.G,
                _gravityConfig.Softening,
                transform.position,
                GravityComponent.Mass,
                other.transform.position,
                other.GravityComponent.Mass
            );

            if (gravitationalForce.magnitude > _strongestGravitationalForce)
            {
                _strongestGravitationalForce = gravitationalForce.magnitude;
                _strongestAttractor = other.GravityComponent;
            }

            _accumulatedForce += gravitationalForce;
        }

        public void ApplyForce()
        {
            _rb.linearVelocity = GravityPhysics.IntegrateVelocity(
                _rb.linearVelocity,
                _accumulatedForce,
                GravityComponent.Mass,
                Time.fixedDeltaTime
            );

            if (_strongestAttractor != null)
            {
                _rb.linearVelocity = GravityOrbitStabilizer.StabilizeVelocity(
                    _gravityConfig.G,
                    orbitStabilizingFactor,
                    _rb.linearVelocity,
                    transform.position,
                    _strongestAttractor.transform.position,
                    _strongestAttractor.Mass,
                    Time.fixedDeltaTime
                );
            }

            _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, maxVelocity);
        }

        public void ResetForce()
        {
            _accumulatedForce = Vector2.zero;
            _strongestAttractor = null;
            _strongestGravitationalForce = 0f;
        }
    }
}