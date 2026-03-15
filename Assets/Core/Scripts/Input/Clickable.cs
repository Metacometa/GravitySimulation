using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GravitySimulator.Input
{
    [RequireComponent(typeof(Collider2D))]
    public class Clickable : MonoBehaviour
    {
        [SerializeField] private List<UnityEvent> onClick;

        private void OnMouseDown()
        {
            foreach (var onClickEvent in onClick)
            {
                onClickEvent?.Invoke();
            }
        }
    }
}