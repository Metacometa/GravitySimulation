using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace GravitySim.Gameplay.Gravity
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