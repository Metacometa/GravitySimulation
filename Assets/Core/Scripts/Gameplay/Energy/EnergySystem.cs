using GravitySimulator.Infrastructure.ComposableBehaviour;
using GravitySimulator.Gameplay.Attraction.Infrastructure;
using UnityEngine;
using Zenject;
using System.Numerics;

namespace GravitySimulator.Gameplay.Energy
{
    public class EnergySystem : MonoBehaviour
    {
        public event Action<int> EnergyUpdated;
            
        private int _energyValue = 0;

        public bool TrySpendEnergy(EnergyCostType energyCostType)
        {
            if (EnergyCostTable.Items.TryGetValue(energyCostType, out int amount) &&
                _energyValue >= amount)
            {
                _energyValue -= amount;
                EnergyUpdated?.Invoke(amount);

                return true;
            }
            
            return false;
        }
    }
}
