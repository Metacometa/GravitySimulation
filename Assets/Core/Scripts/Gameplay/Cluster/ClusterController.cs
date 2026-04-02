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

        private void Awake()
        {
            Cluster cluster = new Cluster();
            _clusterRegistry.Add(cluster);
        }

        public void Spawn()
        {
            foreach (Cluster activeCluster in _clusterRegistry.Active)
            {
                if (activeCluster.IsActive())
                {
                    SpawnNewCluster(activeCluster);
                }   
                else
                {
                    _clusterRegistry.RemoveActive(activeCluster);
                }
            }
        }

        private void SpawnNewCluster(Cluster pivotCluster)
        {
            
        }
    }
}