using UnityEngine;
using System;

namespace GravitySimulator.Gameplay.Energy
{
    public class EnergySystem : MonoBehaviour
    {
        public event Action<int> EnergyUpdated;
            
        private int _energyValue = 50000;

        private void Awake()
        {
            EnergyUpdated?.Invoke(_energyValue);
        }

        public bool TrySpendEnergy(EnergyCostType energyCostType)
        {
            if (EnergyCostTable.Items.TryGetValue(energyCostType, out int amount) &&
                _energyValue >= amount)
            {
                _energyValue -= amount;
                EnergyUpdated?.Invoke(_energyValue);

                return true;
            }
            
            return false;
        }
    }
}
