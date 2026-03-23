using GravitySimulator.Gameplay.Orbital.Core;
using GravitySimulator.Gameplay.Orbital.View.Line;
using GravitySimulator.Gameplay.Orbital.View.Points;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View
{
    public class OrbitView : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }     

        [Header("Component References")]
        [SerializeField] private OrbitPointsView pointsView;
        [SerializeField] private OrbitPathLineView pathLineView;

        public void Initialize(OrbitRoot root)
        {
            Root = root;

            pointsView.Initialize(Root);
            pathLineView.Initialize(Root);
        }

        public void InitializeActions()
        {
            pointsView.InitializeActions();
            pathLineView.InitializeActions();
        }
    }
}