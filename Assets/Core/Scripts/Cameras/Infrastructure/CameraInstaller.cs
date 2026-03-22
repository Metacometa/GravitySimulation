using UnityEngine;
using Zenject;

namespace GravitySimulator.Interaction.Input.Infrastructure
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera camera;

        public override void InstallBindings()
        {
            Container.Bind<Camera>()
                .FromInstance(camera)
                .AsSingle();
        }        
    }
}