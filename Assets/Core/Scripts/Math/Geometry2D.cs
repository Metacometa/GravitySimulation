using System.Collections.Generic;
using GravitySimulator.Gameplay.Orbital.Trajectory.View.Points;
using UnityEngine;

namespace GravitySimulator.Math
{
    public static class Geometry2D
    {
        /// <summary>
        /// One full rotation of a circle
        /// </summary>
        public static float OneFullTurn()
        {
            return 2 * Mathf.PI;
        }

        /// <summary>
        /// Returns a normalized direction vector corresponding to the given angle
        /// on the unit circle (radius = 1, center = (0,0)
        /// </summary>
        public static Vector2 UnitCircle(float angleRad)
        {
            return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));             
        }
        
        /// <summary>
        /// Returns the position of a point on a circle defined by center and radius
        /// at the specified angle.
        /// </summary>
        public static Vector2 PointOnCircle(
            Vector2 center,
            float radius,
            float angleRad
        )
        {
            return center + UnitCircle(angleRad) * radius;
        }

        /// <summary>
        /// Returns a length of a circle)
        /// </summary>
        public static float Circumference(float radius)
        {
            return 2 * Mathf.PI * radius;       
        }

        public static bool TryGetClosestIndex(IReadOnlyList<Vector2> vectors, Vector2 source, out int closestIndex)
        {
            closestIndex = 0;
            float closestDistance = Mathf.Infinity;

            if (vectors.Count <= 0) return false;

            for (int i = 0; i < vectors.Count; ++i)
            {
                float distance = Vector2.Distance(vectors[i], source);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            return true;
        }

        public static bool TryGetClosestIndex(IReadOnlyList<OrbitPointView> vectors, Vector2 source, out int closestIndex)
        {
            closestIndex = 0;
            float closestDistance = Mathf.Infinity;

            if (vectors.Count <= 0) return false;

            for (int i = 0; i < vectors.Count; ++i)
            {
                float distance = Vector2.Distance(vectors[i].Position, source);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            return true;
        }

    }
}