using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Infrastructure.ComposableBehaviour;
using System.Collections.Generic;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View.Line
{
    public class OrbitPathLineView : ComposableChild<OrbitRoot>
    {
        [Header("Component References")]
        [SerializeField] private LineRenderer lineRenderer;
        
        public override void Bind()
        {
            Root.Path.PathUpdated += RebuildLineRenderer;
        }

        public override void Activate()
        {
            SetVisible(false);   
        }

        private void OnDestroy()
        {
            Root.Path.PathUpdated -= RebuildLineRenderer;
        }

        public void ShowOrbit() => SetVisible(true);

        public void HideOrbit() => SetVisible(false);

        private void SetVisible(bool state) => lineRenderer.enabled = state;

        private void RebuildLineRenderer()
        {
            IReadOnlyCollection<Vector2> points = Root.Path.Points.Items;
            lineRenderer.positionCount = points.Count;

            int i = 0;
            foreach (Vector2 point in points)
            {
                lineRenderer.SetPosition(i++, point);
            }            
        }
    }
}