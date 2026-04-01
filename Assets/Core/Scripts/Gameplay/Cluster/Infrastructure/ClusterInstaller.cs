using Zenject;

namespace GravitySimulator.Gameplay.Clusters.Infrastructure
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