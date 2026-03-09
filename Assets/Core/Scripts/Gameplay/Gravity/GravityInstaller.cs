using GravitySimulator.Gameplay.Gravity.Simulation;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Gravity
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

            Container.Bind<GravityBody2D>()
                .FromComponentsInHierarchy()
                .AsTransient();
        }        
    }
}