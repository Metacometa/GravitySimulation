using System;
using UnityEngine;

namespace GravitySimulator.Interaction.Input
{
    [RequireComponent(typeof(Collider2D))]
    public class Draggable : MonoBehaviour
    {
        public event Action DragStarted;
        public event Action Dragged;
        public event Action DragEnded;

        public bool IsDragging { get; private set; }

        private void OnMouseDown() 
        {
            IsDragging = true;
            DragStarted?.Invoke();
        }

        private void OnMouseDrag() => Dragged?.Invoke();
        private void OnMouseUp() 
        {
            IsDragging = false;
            DragEnded?.Invoke();
        }
    }
}