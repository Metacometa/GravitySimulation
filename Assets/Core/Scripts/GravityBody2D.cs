using UnityEngine;

public class GravityBody2D : MonoBehaviour
{
    public float Mass;

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;

    [SerializeField] float orbitAssist = 0.5f;

    [Header("Resources")]
    private GravityConfig _gravityConfig;
    private Rigidbody2D _rb;

    private Vector2 _totalForce;

    private GravityBody2D _strongestAttractor;
    private float _strongestGravity;


    private void Awake()
    {
        _gravityConfig = FindAnyObjectByType<GravityConfig>();
        _rb = GetComponent<Rigidbody2D>();

        if (_rb == null) return;
        _rb.linearVelocity = Random.insideUnitCircle.normalized * speed;
    }

    public void AddForce(GravityBody2D other)
    {
        Vector2 dir = other.transform.position - transform.position;
        float sqrDist = dir.sqrMagnitude;

        float gravity = other.Mass / sqrDist;

        if (gravity > _strongestGravity)
        {
            _strongestGravity = gravity;
            _strongestAttractor = other;
        }

        float force = _gravityConfig.G * (Mass * other.Mass / sqrDist);

        _totalForce += dir.normalized * force;
    }

    public void ApplyForce()
    {
        Debug.Log($"Total Force: {_totalForce}");
        
        if (_rb == null) return;
        _rb.linearVelocity += (_totalForce / Mass) * Time.fixedDeltaTime;

        if (_strongestAttractor != null)
        {
            Vector2 dir = _strongestAttractor.transform.position - transform.position;

            float r = dir.magnitude;

            Vector2 radial = dir.normalized;
            Vector2 tangent = new Vector2(-radial.y, radial.x);

            float orbitalSpeed = Mathf.Sqrt(_gravityConfig.G * _strongestAttractor.Mass / r);

            float currentTangentialSpeed = Vector2.Dot(_rb.linearVelocity, tangent);

            float correctedTangential =
                Mathf.Lerp(currentTangentialSpeed, orbitalSpeed, orbitAssist * Time.fixedDeltaTime);

            Vector2 radialVelocity = Vector2.Dot(_rb.linearVelocity, radial) * radial;

            _rb.linearVelocity = radialVelocity + tangent * correctedTangential;
        }

        _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, maxSpeed);    
    }

    public void ResetForce()
    {
        _totalForce = Vector2.zero;
        _strongestAttractor = null;
        _strongestGravity = 0f;
    }
}
