using GravitySimulator.Gameplay.Cluster.Infrastructure;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Cluster
{
    public class ClusterController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float minDistanceBetweenClusters;

        [Inject] private ClusterRegistry _clusterRegistry;

        public void Spawn()
        {

            _clusterRegistry.Add();
        }
    }
}