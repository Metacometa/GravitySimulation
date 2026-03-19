using System;
using UnityEngine;

namespace GravitySimulator.Interaction.Input
{
    public class InputReader : MonoBehaviour
    {
        public event Action<float> Scroll;

        private void Update()
        {
            float scroll = UnityEngine.Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0)
                Scroll?.Invoke(scroll);
        }
    }
}