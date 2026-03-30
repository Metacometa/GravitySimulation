using GravitySimulator.Infrastructure.ComposableBehaviour;
using GravitySimulator.Gameplay.Attraction.Infrastructure;
using UnityEngine;
using Zenject;
using System.Numerics;

namespace GravitySimulator.Gameplay.Energy
{
    public class EnergyCostTable 
    {
        public static IReadOnlyDictionary<EnergyCostType, int> Items => _energy;
        
        private static readonly Dictionary<EnergyCostType, int> _energy
        {
            { EnergyCostType.OrbitRadiusChanging, 50 },
            { EnergyCostType.AttractorChanging, 100 },
        }
    }

    public enum EnergyCostType
    {
        OrbitRadiusChanging,
        AttractorChanging
    }
}
