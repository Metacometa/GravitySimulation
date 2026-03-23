using System.Collections;
using System.Collections.Generic;
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

            _coroutine = StartCoroutine(EditingStartAnimation(center, points));
        }

        public void AnimateEditingEnd(Vector2 center)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(EditingEndAnimation(center));
        }

        private IEnumerator EditingStartAnimation(Vector2 center, IReadOnlyList<Vector2> points)
        {
            pointPool.ResizePool(points);
            pointPool.SetPosition(center);
         
            IReadOnlyList<GameObject> pool = pointPool.PointsViews;

            HashSet<int> reachEndPointsIndexes = new();

            while (reachEndPointsIndexes.Count < points.Count)
            {
                for (int i = 0; i < points.Count; ++i)
                {
                    if (reachEndPointsIndexes.Contains(i))
                        continue;

                    if (IsReached(pool[i], points[i]))
                    {
                        reachEndPointsIndexes.Add(i);
                        continue;
                    }
                    else
                    {
                        MovePosition(pool[i], points[i]);
                    }
                }

                yield return null;
            }       
        }

        private IEnumerator EditingEndAnimation(Vector2 center)
        {
            IReadOnlyList<GameObject> pool = pointPool.PointsViews;

            HashSet<int> reachEndPointsIndexes = new();

            while (reachEndPointsIndexes.Count < pool.Count)
            {
                for (int i = 0; i < pool.Count; ++i)
                {
                    if (reachEndPointsIndexes.Contains(i))
                        continue;

                    if (IsReached(pool[i], center))
                    {
                        reachEndPointsIndexes.Add(i);
                        continue;
                    }
                    else
                    {
                        MovePosition(pool[i], center);
                    }
                }

                yield return null;
            }       
        }        

        private void MovePosition(GameObject gameObject, Vector2 targetPoint)
        {
            Vector2 newPosition = Vector2.Lerp(gameObject.transform.position, targetPoint, animationSpeed * Time.deltaTime);
            gameObject.transform.position = newPosition;
        }

        private bool IsReached(GameObject gameObject, Vector2 targetPoint, float delta = 0.001f)
        {
            return Vector2.Distance(gameObject.transform.position, targetPoint) <= delta;
        }
    }
}