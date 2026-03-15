using GravitySimulator.Gameplay.Gravity.Infrastructure;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Gameplay.Gravity.Simulation
{
    public class GravityController : MonoBehaviour
    {
        [Inject] private GravityBodyRegistry _gravityBodyRegistry;

        private readonly List<GravityBody2D> _gravityBodiesBuffer = new();

        private void FixedUpdate()
        {
            _gravityBodiesBuffer.Clear();
            _gravityBodiesBuffer.AddRange(_gravityBodyRegistry.GravityBodies);

            foreach (var gravityBody in _gravityBodiesBuffer)
                gravityBody.ResetForce();

            for (int i = 0; i < _gravityBodiesBuffer.Count; ++i)
            {
                for (int j = i + 1; j < _gravityBodiesBuffer.Count; ++j)
                {
                    if (_gravityBodiesBuffer[i].GravityComponent.GravityBodyType == 
                        _gravityBodiesBuffer[j].GravityComponent.GravityBodyType)
                        continue;

                    _gravityBodiesBuffer[i].AddGravitationalForce(_gravityBodiesBuffer[j]);
                    _gravityBodiesBuffer[j].AddGravitationalForce(_gravityBodiesBuffer[i]);
                }
            }

            foreach (var gravityBody in _gravityBodiesBuffer)
                gravityBody.ApplyForce();
        }
    }
}