using System;
using System.Collections.Generic;
using GravitySimulator.Infrastructure.Collections;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View.Points
{
    public class OrbitPointMatcher : MonoBehaviour
    {
        public static void MatchPointsToPositions(
            Vector2 center, 
            CircularList<OrbitPointView> points, 
            CircularList<Vector2> targets
        )
        {
            if (points.Count != targets.Count)
            {
                throw new IndexOutOfRangeException("Get Matched points gotten lists of inequals sizes");
            }

            if (Math.Geometry2D.TryGetClosestIndex(points.Items, center, out int pointsIndex) &&
                Math.Geometry2D.TryGetClosestIndex(targets.Items, points.Items[pointsIndex].Position, out int targetsIndex))
            {
                IReadOnlyList<OrbitPointView> matchingPoints = points.GetItemsFrom(pointsIndex);
                IReadOnlyList<Vector2> matchingTargets = targets.GetItemsFrom(targetsIndex);

                for (int i = 0; i < matchingPoints.Count; ++i)
                {
                    matchingPoints[i].ClearTargets();
                }

                for (int i = 1; i < matchingPoints.Count; ++i)
                {
                    matchingPoints[i].AddTarget(matchingPoints[i - 1].transform);
                }

                for (int i = 0; i < matchingPoints.Count; ++i)
                {
                    matchingPoints[i].AddTarget(matchingTargets[i]);
                }
            }
        }
    }
}