using UnityEngine;

namespace GravitySimulator.Gameplay.Gravity
{
    public class GravityComponent : MonoBehaviour
    {
        public GravityBodyType GravityBodyType => gravityBodyType;
        public float Mass => mass;

        [SerializeField] private GravityBodyType gravityBodyType;
        
        [SerializeField, Min(0.0001f)]
        private float mass;
    }
}