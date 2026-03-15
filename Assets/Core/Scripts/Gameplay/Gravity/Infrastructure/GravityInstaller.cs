using GravitySimulator.Gameplay.Gravity.Config;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Gravity.Infrastructure
{
    public class GravityInstaller : MonoInstaller
    {
        [Header("Config")]
        [SerializeField] private GravityConfig gravityConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(gravityConfig).AsSingle();

            Container.Bind<GravityBodyRegistry>()
                .AsSingle();
        }        
    }
}