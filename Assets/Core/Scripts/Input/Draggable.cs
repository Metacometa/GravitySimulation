using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GravitySimulator.Input
{
    [RequireComponent(typeof(Collider2D))]
    public class Draggable : Clickable
    {
        [SerializeField] private List<UnityEvent> onDrag;
        [SerializeField] private List<UnityEvent> onDragEnd;

        private void OnMouseDrag()
        {
            foreach (var onDragEvent in onDrag)
            {
                onDragEvent?.Invoke();
            }

            Vector3 mouse = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            mouse.z = transform.position.z;

            transform.position = mouse;
        }

        private void OnMouseUp()
        {
            foreach (var onDragEndEvent in onDragEnd)
            {
                onDragEndEvent?.Invoke();
            }
        }
    }
}