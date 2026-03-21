using GravitySimulator.Gameplay.Gravity.Config;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Gravity.Infrastructure
{
    public class AttractionInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AttractorRegistry>()
                .AsSingle();
        }        
    }
}