using UnityEngine;
using Zenject;

namespace GravitySimulator.Interaction.Input.Infrastructure
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private UnityEngine.Camera camera;

        public override void InstallBindings()
        {
            Container.Bind<UnityEngine.Camera>()
                .FromInstance(camera)
                .AsSingle();
        }        
    }
}