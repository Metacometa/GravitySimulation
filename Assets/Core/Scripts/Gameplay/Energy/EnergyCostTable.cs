using System.Collections.Generic;

namespace GravitySimulator.Gameplay.Energy
{
    public class EnergyCostTable 
    {
        public static IReadOnlyDictionary<EnergyCostType, int> Items => _energy;
        
        private static readonly Dictionary<EnergyCostType, int> _energy = new()
        {
            { EnergyCostType.OrbitRadiusChanging, 50 },
            { EnergyCostType.AttractorChanging, 100 },
        };
    }

    public enum EnergyCostType
    {
        OrbitRadiusChanging,
        AttractorChanging
    }
}
