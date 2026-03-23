using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Gameplay.Orbital.View.Points;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View
{
    public class OrbitView : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }     

        [Header("Component References")]
        [SerializeField] private OrbitPathPointsView pathPointsView;
        [SerializeField] private OrbitPathLineView pathLineView;

        public void Initialize(OrbitRoot root)
        {
            Root = root;

            pathPointsView.Initialize(Root);
            pathLineView.Initialize(Root);
        }

        public void InitializeActions()
        {
            pathPointsView.InitializeActions();
            pathLineView.InitializeActions();
        }
    }
}