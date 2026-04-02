using GravitySimulator.Gameplay.Clusters.Infrastructure;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Clusters
{
    public class Cluster
    {
        public bool IsActive => _isActive ? CheckCluster() : false;

        public Vector2 Center => _center;

        [Inject] private ClusterRegistry _clusterRegistry;
        [Inject] private ClusterController _clusterController;

        private Vector2 _center;

        private bool _isActive = true;

        private bool CheckCluster()
        {
            for (float deg = 0f; deg <= 360f; deg += 45f)
            {
                Vector2 neighbour = Math.Geometry2D.PointOnCircle(Center, _clusterController.MinDistanceBetweenClusters, deg * Mathf.Deg2Rad);
                if (CheckActivity(neighbour))
                    return true;
            }

            return false;
        }

        private bool CheckActivity(Vector2 vectorToCheck)
        {
            foreach (Cluster cluster in _clusterRegistry.Active)
            {
                float distance = Vector2.Distance(vectorToCheck, cluster.Center);

                if (distance < _clusterController.MinDistanceBetweenClusters)
                    return false;
            }

            return true;
        }
    }
}