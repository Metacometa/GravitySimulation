using UnityEngine;

namespace GravitySimulator.Math
{
    public static class Geometry2D
    {
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
            float angleRad,
            float radius
        )
        {
            return center + UnitCircle(angleRad) * radius;
        }
    }
}