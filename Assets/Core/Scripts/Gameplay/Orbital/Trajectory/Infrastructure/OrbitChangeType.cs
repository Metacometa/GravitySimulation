using System;

namespace GravitySimulator.Gameplay.Orbital.Trajectory.Infrastructure
{
    public enum OrbitChangeType
    {
        Radius,
        Attractor
    }

    public static class OrbitChangeTypeExtensions
    {
        public static OrbitPathUpdateType ToPathUpdateType(this OrbitChangeType changeType)
        {
            return changeType switch
            {
                OrbitChangeType.Radius => OrbitPathUpdateType.Radius,
                OrbitChangeType.Attractor => OrbitPathUpdateType.Attractor,
                _ => throw new ArgumentOutOfRangeException(nameof(changeType), changeType, null)
            };
        }
    }
}