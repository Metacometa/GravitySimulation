using System;
using GravitySimulator.Interaction.Input;
using UnityEngine;

namespace GravitySimulator.Interaction.Drag
{
    public class DragMove : MonoBehaviour
    {
        public event Action Moved;

        [Header("Component References")]
        [SerializeField] private Draggable draggable;

        private void Awake()
        {
            if (draggable == null)
            {
                Debug.LogError($"{nameof(DragMove)}: Draggable not assigned", this);
                enabled = false;
                return;
            }

            draggable.DragStarted += OnMove;
            draggable.Dragged += OnMove;
            draggable.DragEnded += OnMove;
        }

        private void OnDestroy()
        {
            if (draggable == null) return;

            draggable.DragStarted -= OnMove;
            draggable.Dragged -= OnMove;
            draggable.DragEnded -= OnMove;
        }
        
        private void OnMove()
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            mouse.z = transform.position.z;
            transform.position = mouse;

            Moved?.Invoke();
        }
    }
}