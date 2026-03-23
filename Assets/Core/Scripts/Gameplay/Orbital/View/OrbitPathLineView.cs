using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Gameplay.Orbital.Mechanics;
using System.Collections.Generic;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View
{
    public class OrbitPathLineView : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }     

        [Header("Component References")]
        [SerializeField] private LineRenderer lineRenderer;
    
        public void Initialize(OrbitRoot root)
        {
            Root = root;
        }
        
        public void InitializeActions()
        {
            Root.Path.PathUpdated += RebuildLineRenderer;
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
            IReadOnlyCollection<Vector2> points = Root.Path.Points;
            lineRenderer.positionCount = points.Count;

            int i = 0;
            foreach (Vector2 point in points)
            {
                lineRenderer.SetPosition(i++, point);
            }            
        }
    }
}