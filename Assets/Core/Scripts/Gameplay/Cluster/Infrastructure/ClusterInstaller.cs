using Zenject;

namespace GravitySimulator.Gameplay.Cluster.Infrastructure
{
    public class ClusterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ClusterRegistry>()
                .AsSingle();
        }   
    }
}