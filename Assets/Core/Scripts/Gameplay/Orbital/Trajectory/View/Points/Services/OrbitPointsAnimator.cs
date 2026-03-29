using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using GravitySimulator.Infrastructure.Collections;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.Trajectory.View.Points
{
    public class OrbitPointsAnimator : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float animationSpeed;

        [Header("Component References")]
        [SerializeField] private OrbitPointPool pointPool;

        private Coroutine _coroutine;

        public void AnimateEditingStart(Vector2 center, CircularList<Vector2> targets)
        {
            StopCurrentAnimation();
            pointPool.ResizePool(targets.Items);

            CircularList<OrbitPointView> pointViews = pointPool.PointsViews;
            for (int i = 0; i < pointViews.Count; ++i)
            {
                pointViews[i].ClearTargets();
                pointViews[i].SetPosition(center);
                pointViews[i].AddTarget(targets[i]);
            }

            _coroutine = StartCoroutine(AnimateMotionToTargets(pointViews));
        }

        public void AnimateRadiusEditing(CircularList<Vector2> targets)
        {
            StopCurrentAnimation();
            pointPool.ResizePool(targets.Items);

            CircularList<OrbitPointView> pointViews = pointPool.PointsViews;
            for (int i = 0; i < pointViews.Count; ++i)
            {
                pointViews[i].ClearTargets();
                pointViews[i].AddTarget(targets[i]);
            }

            _coroutine = StartCoroutine(AnimateMotionToTargets(pointViews));
        }

        public void AnimateCenterChangedEditing(Vector2 center, CircularList<Vector2> targets)
        {
            StopCurrentAnimation();
            pointPool.ResizePool(targets.Items);

            OrbitPointMatcher.MatchPointsToPositions(center, pointPool.PointsViews, targets);

            _coroutine = StartCoroutine(AnimateMotionToTargets(pointPool.PointsViews));
        }

        public void AnimateEditingEnd(Vector2 center)
        {
            StopCurrentAnimation();

            CircularList<OrbitPointView> pointViews = pointPool.PointsViews;
            for (int i = 0; i < pointViews.Count; ++i)
            {
                pointViews[i].ClearTargets();
                pointViews[i].AddTarget(center);
            }

            _coroutine = StartCoroutine(AnimateMotionToTargets(pointViews));
        }

        private IEnumerator AnimateMotionToTargets(CircularList<OrbitPointView> pointViews)
        {
            HashSet<int> reachEndPointsIndexes = new();

            while (reachEndPointsIndexes.Count < pointViews.Count)
            {
                for (int i = 0; i < pointViews.Count; ++i)
                {
                    if (reachEndPointsIndexes.Contains(i))
                        continue;

                    if (pointViews[i].HasTargetReached)
                    {
                        reachEndPointsIndexes.Add(i);
                    }   
                    else
                    {
                        pointViews[i].TickMove(animationSpeed);
                    }
                }

                yield return null;
            }       
        }      

        // private IEnumerator AnimateChainMotionToTargets(CircularList<OrbitPointView> pointViews)
        // {
        //     HashSet<int> reachEndPointsIndexes = new();

        //     while (reachEndPointsIndexes.Count < pointViews.Count)
        //     {
        //         for (int i = 0; i < pointViews.Count; ++i)
        //         {
        //             if (reachEndPointsIndexes.Contains(i))
        //                 continue;

        //             if (pointViews[i].HasTargetReached)
        //             {
        //                 reachEndPointsIndexes.Add(i);
        //                 continue;
        //             }   

        //             pointViews[i].TickMove(animationSpeed);   
        //         }

        //         yield return null;
        //     }      
        // }

        private void StopCurrentAnimation()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }
    }
}