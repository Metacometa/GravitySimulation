using System;
using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Gameplay.Orbital.View;
using GravitySimulator.Interaction.Input;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Orbital.Interaction
{
    public class OrbitEditor : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }     

        public event Action EditStart;

        public event Action<float> EditRadius;
        public event Action EditPosition;

        public event Action EditEnd;

        [Header("Component References")]
        [SerializeField] private Draggable draggable;

        [Inject] private InputReader _inputReader;

        public void Initialize(OrbitRoot root)
        {
            Root = root;
        }

        public void InitializeActions()
        {
            draggable.DragStart += OnEditStart;

            _inputReader.Scroll += OnEditRadius;

            draggable.DragStart += OnEditPosition;
            draggable.Drag += OnEditPosition;
            draggable.DragEnd += OnEditPosition;

            draggable.DragEnd += OnEditEnd;            
        }


        private void OnDestroy()
        {
            draggable.DragStart -= OnEditStart;

            _inputReader.Scroll -= OnEditRadius;

            draggable.DragStart -= OnEditPosition;
            draggable.Drag -= OnEditPosition;
            draggable.DragEnd -= OnEditPosition;

            draggable.DragEnd -= OnEditEnd;
        }

        private void OnEditStart()
        {
            EditStart?.Invoke();
        }

        private void OnEditRadius(float radiusDelta)
        {
            if (draggable.IsDragging)
            {
                float invertedRadiusDelta = -radiusDelta;
                EditRadius?.Invoke(invertedRadiusDelta);
            }
        }

        private void OnEditPosition()
        {
            EditPosition?.Invoke();
        }

        private void OnEditEnd()
        {
            EditEnd?.Invoke();
        }
    }
}