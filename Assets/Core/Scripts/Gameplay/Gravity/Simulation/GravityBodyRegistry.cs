using System.Collections.Generic;

namespace GravitySimulator.Gameplay.Gravity.Simulation
{
    public class GravityBodyRegistry
    {
        public readonly HashSet<GravityBody2D> Bodies = new();
    }
}