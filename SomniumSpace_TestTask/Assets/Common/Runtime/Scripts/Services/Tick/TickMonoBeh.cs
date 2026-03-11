using UnityEngine;

namespace Common.Services.Tick
{
    public class TickMonoBeh : MonoBehaviour
    {
        private TickService _tickService;

        public void Init(TickService tickService)
        {
            Debug.Log("TickMonoBeh Initss");
            _tickService = tickService;
            gameObject.name = "TickMonoBeh";
            DontDestroyOnLoad(gameObject);
        }

        private void Update() => _tickService.Tick();
        private void FixedUpdate() => _tickService.FixedTick();
        private void LateUpdate() => _tickService.LateTick();
    }
}