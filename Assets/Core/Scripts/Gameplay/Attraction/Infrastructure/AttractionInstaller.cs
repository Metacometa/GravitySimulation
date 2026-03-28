using GravitySimulator.Gameplay.Gravity.Config;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Attraction.Infrastructure
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