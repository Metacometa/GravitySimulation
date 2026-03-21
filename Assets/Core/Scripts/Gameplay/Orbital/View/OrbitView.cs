using GravitySimulator.Gameplay.Orbital.Core;
using UnityEngine;

namespace GravitySimulator.Gameplay.Orbital.View
{
    public class OrbitView : MonoBehaviour
    {
        public OrbitRoot Root { get; private set; }     

        public OrbitPathView PathView => pathView;
        
        [Header("Component References")]
        [SerializeField] private OrbitPathView pathView;

        public void Initialize(OrbitRoot root)
        {
            Root = root;

            pathView.Initialize(Root);
        }
    }
}