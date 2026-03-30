using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Energy.Infrastructure
{
    public class EnergyInstaller : MonoInstaller
    {
        [Header("Component References")]
        [SerializeField] private EnergySystem energySystem;

        public override void InstallBindings()
        {
            Container.BindInstance(energySystem).AsSingle();
        }        
    }
}