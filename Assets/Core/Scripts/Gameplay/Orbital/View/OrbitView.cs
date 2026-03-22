using GravitySimulator.Gameplay.Orbital.Core;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View
{
    public class OrbitView : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }     

        public OrbitPathPointsView PathPointsView => pathPointsView;
        public OrbitPathLineView PathLineView => pathLineView;

        [Header("Component References")]
        [SerializeField] private OrbitPathPointsView pathPointsView;
        [SerializeField] private OrbitPathLineView pathLineView;

        public void Initialize(OrbitRoot root)
        {
            Root = root;

            pathPointsView.Initialize(Root);
            pathLineView.Initialize(Root);
        }
    }
}