using Fusion;
using TMPro;
using UnityEngine;

namespace SomniumSpace.Services.Networks
{
    public class PlayerNameLabel : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;

        private Camera _mainCamera;

        public override void Spawned()
        {
            _mainCamera = Camera.main;

            string displayName = HasStateAuthority
                ? $"You ({Object.InputAuthority})"
                : $"Player {Object.InputAuthority}";

            if (label != null)
                label.text = displayName;
        }

        private void LateUpdate()
        {
            if (_mainCamera == null || label == null) return;

            label.transform.rotation = Quaternion.LookRotation(
                label.transform.position - _mainCamera.transform.position
            );
        }
    }
}