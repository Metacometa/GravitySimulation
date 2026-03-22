using GravitySimulator.Interaction.Input;
using UnityEngine;
using Zenject;

namespace GravitySimulator.Cameras
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float zoomSpeed;

        [Inject] private Camera _camera;

        [Inject] private InputReader inputReader;

        private void Awake()
        {
            inputReader.CtrlScrolled += Zoom;
        }

        private void OnDestroy()
        {
            inputReader.CtrlScrolled -= Zoom;
        }

        private void Zoom(float value)
        {
            _camera.orthographicSize += value * zoomSpeed;
        }
    }
}
