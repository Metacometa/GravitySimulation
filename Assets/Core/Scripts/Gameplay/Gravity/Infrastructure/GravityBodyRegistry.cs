using GravitySimulator.Gameplay.Gravity.Simulation;
using System.Collections.Generic;

namespace GravitySimulator.Gameplay.Gravity.Infrastructure
{
    public class GravityBodyRegistry
    {
        public readonly HashSet<GravityBody> GravityBodies = new();
    }
}