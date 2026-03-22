using System;
using UnityEngine;

namespace GravitySimulator.Interaction.Input
{
    public class InputReader : MonoBehaviour
    { 
        public event Action<float> Scroll;
        public event Action<float> CtrlScroll;

        private void Update()
        {
            float scroll = -UnityEngine.Input.GetAxis("Mouse ScrollWheel");
            bool ctrl = UnityEngine.Input.GetKey(KeyCode.LeftControl) ||
                        UnityEngine.Input.GetKey(KeyCode.RightControl);

            if (scroll != 0)
            {
                if (ctrl)
                    CtrlScroll?.Invoke(scroll);
                else
                    Scroll?.Invoke(scroll);
            }
        }
    }
}