using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GravitySim.Gameplay.Gravity
{
    public class GravityBodyRegistry
    {
        public readonly HashSet<GravityBody2D> Bodies = new();
    }
}