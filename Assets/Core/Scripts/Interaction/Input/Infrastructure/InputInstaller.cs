using Zenject;

namespace GravitySimulator.Interaction.Input.Infrastructure
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InputReader>()
                .FromNewComponentOnNewGameObject()
                .AsSingle();
        }        
    }
}