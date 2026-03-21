using System;
using UnityEngine;

namespace GravitySimulator.Interaction.Input
{
    [RequireComponent(typeof(Collider2D))]
    public class Draggable : MonoBehaviour
    {
        public event Action DragStart;
        public event Action Drag;
        public event Action DragEnd;

        public bool IsDragging { get; private set; }

        private void OnMouseDown() 
        {
            IsDragging = true;
            DragStart?.Invoke();
        }

        private void OnMouseDrag() => Drag?.Invoke();
        private void OnMouseUp() 
        {
            IsDragging = false;
            DragEnd?.Invoke();
        }
    }
}