using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace GravitySim.Gameplay.Gravity
{
    public class GravityController : MonoBehaviour
    {
        [Inject] private GravityBodyRegistry _gravityBodyRegistry;

        private readonly List<GravityBody2D> _gravityBodiesBuffer = new();

        private void FixedUpdate()
        {
            _gravityBodiesBuffer.Clear();
            _gravityBodiesBuffer.AddRange(_gravityBodyRegistry.Bodies);

            foreach (var gravityBody in _gravityBodiesBuffer)
                gravityBody.ResetForce();

            for (int i = 0; i < _gravityBodiesBuffer.Count; ++i)
            {
                for (int j = i + 1; j < _gravityBodiesBuffer.Count; ++j)
                {
                    _gravityBodiesBuffer[i].AddForce(_gravityBodiesBuffer[j]);
                    _gravityBodiesBuffer[j].AddForce(_gravityBodiesBuffer[i]);
                }
            }

            foreach (var gravityBody in _gravityBodiesBuffer)
                gravityBody.ApplyForce();
        }
    }
}