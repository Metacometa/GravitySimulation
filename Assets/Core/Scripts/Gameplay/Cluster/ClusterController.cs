using GravitySimulator.Gameplay.Clusters.Infrastructure;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Clusters
{
    public class ClusterController : MonoBehaviour
    {
        public float MinDistanceBetweenClusters => minDistanceBetweenClusters;

        [Header("Settings")]
        [SerializeField] private float minDistanceBetweenClusters;

        [Inject] private ClusterRegistry _clusterRegistry;

        // private

        public void Spawn()
        {
            // Cluster kek = 
            // _clusterRegistry.Add();
        }
    }
}