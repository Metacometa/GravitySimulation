using System;
using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Gameplay.Orbital.View;
using GravitySimulator.Infrastructure.ComposableBehaviour;
using GravitySimulator.Interaction.Drag;
using GravitySimulator.Interaction.Input;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Orbital.Interaction
{
    public class OrbitEditor : ComposableChild<OrbitRoot>
    {
        public event Action EditStarted;

        public event Action<float> RadiusEdited;
        public event Action PositionEdited;

        public event Action EditEnded;

        public bool IsEditing = false;

        [Header("Component References")]
        [SerializeField] private Draggable draggable;
        [SerializeField] private DragMove dragMove;

        [Inject] private InputReader _inputReader;

        public override void Bind()
        {
            draggable.DragStarted += OnEditStarted;

            _inputReader.Scrolled += OnRadiusEdited;
            dragMove.Moved += OnPositionEdited;

            draggable.DragEnded += OnEditEnded;            
        }

        private void OnDestroy()
        {
            draggable.DragStarted -= OnEditStarted;

            _inputReader.Scrolled -= OnRadiusEdited;
            dragMove.Moved -= OnPositionEdited;

            draggable.DragEnded -= OnEditEnded;
        }

        private void OnEditStarted()
        {
            EditStarted?.Invoke();
            IsEditing = true;
        }

        private void OnRadiusEdited(float radiusDelta)
        {
            if (draggable.IsDragging)
            {
                float invertedRadiusDelta = -radiusDelta;
                RadiusEdited?.Invoke(invertedRadiusDelta);
            }
        }

        private void OnPositionEdited()
        {
            PositionEdited?.Invoke();
        }

        private void OnEditEnded()
        {
            EditEnded?.Invoke();
            IsEditing = false;
        }
    }
}