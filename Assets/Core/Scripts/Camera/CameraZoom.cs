using GravitySimulator.Interaction.Input;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Camera
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float zoomSpeed;

        [Inject] private UnityEngine.Camera camera;

        [Inject] private InputReader inputReader;

        private void Awake()
        {
            inputReader.CtrlScroll += Zoom;
        }

        private void Zoom(float value)
        {
            camera.orthographicSize += value * zoomSpeed;
        }
    }
}
