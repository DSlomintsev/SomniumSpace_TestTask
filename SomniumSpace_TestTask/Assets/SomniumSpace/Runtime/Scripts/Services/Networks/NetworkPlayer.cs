using Common.Models;
using Fusion;
using SomniumSpace.Configs;
using SomniumSpace.Models;
using UnityEngine;

namespace SomniumSpace.Services.Networks
{
    [RequireComponent(typeof(NetworkObject))]
    [RequireComponent(typeof(PlayerMovement))]
    public class NetworkedPlayer : NetworkBehaviour
    {
        [Networked] private Vector3 NetworkedPosition { get; set; }
        [Networked] private Quaternion NetworkedRotation { get; set; }

        private PlayerMovement _movement;
        private Transform _visualRoot; // child transform used for interpolation
        private ChangeDetector _changeDetector;
        private NetworkConfigSO _networkConfig;

        public void Awake()
        {
            _networkConfig = ModelsLocator.Get<GameModel>().Configs.Network;
        }

        public override void Spawned()
        {
            _movement = GetComponent<PlayerMovement>();
            _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);

            var visual = transform.Find("Visual");
            _visualRoot = visual != null ? visual : transform;

            if (HasStateAuthority)
            {
                NetworkedPosition = transform.position;
                NetworkedRotation = transform.rotation;

                var cam = FindFirstObjectByType<CameraFollow>();
                cam?.SetTarget(transform);

                gameObject.name = $"Player_Local_{Object.InputAuthority}";
            }
            else
            {
                gameObject.name = $"Player_Remote_{Object.InputAuthority}";
                // Disable physics / character controller on remote copies.
                _movement.enabled = false;
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (!HasStateAuthority) return;

            _movement.Tick(Runner.DeltaTime);

            // Write physics result to networked properties.
            NetworkedPosition = transform.position;
            NetworkedRotation = transform.rotation;
        }

        public override void Render()
        {
            if (HasStateAuthority) return; // exclude local player

            // Smoothly move the visual toward the latest authoritative state.
            _visualRoot.position = Vector3.Lerp(
                _visualRoot.position,
                NetworkedPosition,
                _networkConfig.PositionInterpolationSpeed * Time.deltaTime
            );

            _visualRoot.rotation = Quaternion.Slerp(
                _visualRoot.rotation,
                NetworkedRotation,
                _networkConfig.RotationInterpolationSpeed * Time.deltaTime
            );
        }
    }
}