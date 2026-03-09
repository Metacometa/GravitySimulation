using UnityEngine;

namespace GravitySimulator.Gameplay.Gravity.Physics
{
    public static class GravityPhysics
    {
        /// <summary>
        /// Simulate movement by Euler's Method using time discretization.
        /// </summary>
        /// <remarks>
        /// Formula: <br/>
        /// a = F / m <br/>
        /// V = V + a * dt
        /// </remarks>
        public static Vector2 IntegrateVelocity(
            in Vector2 velocity,
            in Vector2 force,
            float mass,
            float dt
        )
        {
            Vector2 acceleration = force / mass;
            return velocity + acceleration * dt;
        }

        /// <summary>
        /// Calculates the Newtonian gravitational force applied by other on body.
        /// </summary>
        /// <remarks>
        /// Formula: <br/>
        /// F = G * m₁ * m₂ / r²
        /// </remarks>
        public static Vector2 GravitationalForceOn(
            float G, 
            float softening,
            in Vector2 bodyPos, 
            float bodyMass,
            in Vector2 attractorPos,
            float attractorMass
        )
        {
            Vector2 dir = attractorPos - bodyPos;
            
            // Softening prevents extreme forces when bodies get very close
            float softenedSqrRadius = dir.sqrMagnitude + softening;
            
            float force = G * (bodyMass * attractorMass / softenedSqrRadius);
            return dir.normalized * force;
        }

        /// <summary>
        /// Calculates the velocity required for a circular orbit.
        /// </summary>
        /// <remarks>
        /// Formula: <br/>
        /// v = √(G · M / r)
        /// </remarks>
        public static float CircularOrbitSpeed(
            float G,
            float attractorMass,
            float radius)
        {
            return Mathf.Sqrt(G * attractorMass / radius);
        }
    }
}