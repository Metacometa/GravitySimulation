using GravitySimulator.Gameplay.Gravity.Physics;
using UnityEngine;

namespace GravitySimulator.Gameplay.Gravity.Mechanics
{
    public static class OrbitStabilizer
    {
        /// <summary>
        /// Stabilizes velocity toward circular orbit around an attractor
        /// </summary>
        public static Vector2 StabilizeVelocity(
            float G,
            float orbitStabilizingFactor,
            in Vector2 velocity,
            in Vector2 bodyPos,
            in Vector2 attractorPos,
            float attractorMass,
            float dt
        )
        {
            Vector2 dir = attractorPos - bodyPos;
            
            float radius = dir.magnitude;
            if (radius < 0.01f)
                return velocity;

            // Radial direction (attractor → body)
            Vector2 radial = dir.normalized;
            // Tangential direction (along the orbit)
            Vector2 tangent = Vector2.Perpendicular(radial);

            if (Vector2.Dot(velocity, tangent) < 0)
                tangent = -tangent;

            float radialSpeed = Vector2.Dot(velocity, radial);  
            float tangentialSpeed = Vector2.Dot(velocity, tangent);

            float desiredOrbitalSpeed = GravityPhysics.CircularOrbitSpeed(G, attractorMass, radius);
            float correction =
                (desiredOrbitalSpeed - tangentialSpeed) *
                orbitStabilizingFactor * dt;

            float stabilizedTangentialSpeed = tangentialSpeed + correction;

            return radial * radialSpeed + tangent * stabilizedTangentialSpeed;
        }
    }
}