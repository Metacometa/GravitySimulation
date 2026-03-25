using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View.Points
{
    public class OrbitPointsAnimator : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float animationSpeed;

        [Header("Component References")]
        [SerializeField] private OrbitPointPool pointPool;

        private Coroutine _coroutine;

        public void AnimateEditingStart(Vector2 center, IReadOnlyList<Vector2> points)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            pointPool.ResizePool(points);
            IReadOnlyList<OrbitPointView> pointViews = pointPool.PointsViews;

            for (int i = 0; i < pointViews.Count; ++i)
            {
                pointViews[i].SetPosition(center);
                pointViews[i].SetTarget(points[i]);
            }

            _coroutine = StartCoroutine(AnimateMotionToTargets());
        }

        public void AnimateRadiusEditing(IReadOnlyList<Vector2> points)
        {
            StopCurrentAnimtion();

            IReadOnlyList<OrbitPointView> pointViews = pointPool.PointsViews;

            for (int i = 0; i < pointViews.Count; ++i)
            {
                pointViews[i].SetTarget(points[i]);
            }

            _coroutine = StartCoroutine(AnimateMotionToTargets());
        }

        public void AnimateCenterChangedEditing(IReadOnlyList<Vector2> points)
        {
            StopCurrentAnimtion();

            IReadOnlyList<OrbitPointView> pointViews = pointPool.PointsViews;

            for (int i = 0; i < pointViews.Count; ++i)
            {
                pointViews[i].SetTarget(points[i]);
            }

            _coroutine = StartCoroutine(AnimateChainMotionToTargets());
        }

        public void AnimateEditingEnd(Vector2 center)
        {
            StopCurrentAnimtion();

            IReadOnlyList<OrbitPointView> pointViews = pointPool.PointsViews;

            for (int i = 0; i < pointViews.Count; ++i)
            {
                pointViews[i].SetTarget(center);
            }

            _coroutine = StartCoroutine(AnimateMotionToTargets());
        }

        private IEnumerator AnimateMotionToTargets()
        {
            IReadOnlyList<OrbitPointView> pointViews = pointPool.PointsViews;
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

        private IEnumerator AnimateChainMotionToTargets()
        {
            IReadOnlyList<OrbitPointView> pointViews = pointPool.PointsViews;
            HashSet<int> reachEndPointsIndexes = new();

            while (reachEndPointsIndexes.Count < pointViews.Count)
            {
                if (pointViews[0].HasTargetReached)
                {
                    reachEndPointsIndexes.Add(0);
                }   
                else
                {
                    pointViews[0].TickMove(animationSpeed);
                }

                for (int i = 1; i < pointViews.Count; ++i)
                {
                    if (reachEndPointsIndexes.Contains(i))
                        continue;

                    if (pointViews[i].HasTargetReached)
                    {
                        reachEndPointsIndexes.Add(i);
                    }   
                    else
                    {   
                        if (pointViews[i - 1].HasTargetReached)
                        {
                            pointViews[i].TickMove(animationSpeed);  
                        }
                        else
                        {
                            pointViews[i].TickMove(animationSpeed, pointViews[i - 1].transform.position); 
                        }
                    }
                }

                yield return null;
            }      
        }

        private void StopCurrentAnimtion()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }
    }
}