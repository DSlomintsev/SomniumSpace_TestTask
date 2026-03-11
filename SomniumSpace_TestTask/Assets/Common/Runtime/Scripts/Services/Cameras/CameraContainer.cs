using UnityEngine;

namespace Common.Services.Cameras
{
    public class CameraContainer:MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        public Camera Camera => _camera;
    }
}
