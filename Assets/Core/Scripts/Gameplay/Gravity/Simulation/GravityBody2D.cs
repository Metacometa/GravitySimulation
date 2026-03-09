using GravitySimulator.Gameplay.Gravity.Mechanics;
using GravitySimulator.Gameplay.Gravity.Physics;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Gravity.Simulation
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GravityBody2D : MonoBehaviour
    {
        public GravityBodyType GravityBodyType => gravityBodyType;
        public float Mass => mass;

        [Header("Body")]
        [SerializeField] private GravityBodyType gravityBodyType;

        [SerializeField, Min(0.0001f)] 
        private float mass;

        [Header("Movement")]
        [SerializeField] private float startSpeed;
        [SerializeField] private float maxVelocity;

        [Header("Orbit Stabilizing")]
        [SerializeField, Range(0f, 10f)] 
        private float orbitStabilizingFactor;

        [Inject] private GravityBodyRegistry _gravityBodyRegistry;
        [Inject] private GravityConfig _gravityConfig;

        private Rigidbody2D _rb;

        private Vector2 _accumulatedForce;
        private GravityBody2D _strongestAttractor;
        private float _strongestGravitationalForce;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            // _rb.linearVelocity = Random.insideUnitCircle.normalized * startSpeed;
        }

        private void OnEnable()
        {
            _gravityBodyRegistry.Bodies.Add(this);
        }

        private void OnDisable()
        {
            _gravityBodyRegistry.Bodies.Remove(this);
        }

        public void AddGravitationalForce(GravityBody2D other)
        {
            Vector2 gravitationalForce = GravityPhysics.GravitationalForceOn(
                _gravityConfig.G, 
                _gravityConfig.Softening,
                transform.position,
                Mass,
                other.transform.position,
                other.Mass
            );

            if (gravitationalForce.magnitude > _strongestGravitationalForce)
            {
                _strongestGravitationalForce = gravitationalForce.magnitude;
                _strongestAttractor = other;                
            }

            _accumulatedForce += gravitationalForce;
        }

        public void ApplyForce()
        {
            _rb.linearVelocity = GravityPhysics.IntegrateVelocity(
                _rb.linearVelocity,
                _accumulatedForce,
                Mass,
                Time.fixedDeltaTime
            );

            if (_strongestAttractor != null)
            {
                _rb.linearVelocity = OrbitStabilizer.StabilizeVelocity(
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