using System.Collections.Generic;
using UnityEngine;

namespace GravitySimulator.Infrastructure.ComposableBehaviour
{
    public class SceneCompositionBootstrap : MonoBehaviour
    {
        private List<ComposableRoot> _roots = new();

        private void Start()
        {
            _roots = new List<ComposableRoot>(FindObjectsByType<ComposableRoot>(FindObjectsSortMode.None));

            foreach (var root in _roots)
                root.Initialize();

            foreach (var root in _roots)
                root.Bind();

            foreach (var root in _roots)
                root.Activate();
        }
    }
}