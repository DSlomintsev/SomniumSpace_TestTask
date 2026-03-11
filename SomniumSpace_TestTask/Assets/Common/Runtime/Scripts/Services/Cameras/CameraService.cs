using Common.Utils;
using UnityEngine;

namespace Common.Services.Cameras
{
    public class CameraService:BaseService
    {
        private CameraContainer _container;
        public CameraContainer Container=>_container;

        public void Construct(CameraContainer cameraContainerPrefab)
        {
            _container = SpawnUtils.Instantiate(cameraContainerPrefab);
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            GameObject.DontDestroyOnLoad(_container);
        }
    }
}
