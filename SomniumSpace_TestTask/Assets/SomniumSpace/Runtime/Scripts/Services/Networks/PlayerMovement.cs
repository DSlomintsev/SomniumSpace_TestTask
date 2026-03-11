using Common.Models;
using SomniumSpace.Configs;
using SomniumSpace.Models;
using UnityEngine;

namespace SomniumSpace.Services.Networks
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController _controller;

        private Vector2 _moveInput;
        private float _rotateInput;
        private NetworkConfigSO _networkConfig;

        private void Awake()
        {
            _networkConfig = ModelsLocator.Get<GameModel>().Configs.Network;
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            _moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _rotateInput = Input.GetAxis("Mouse X");
        }

        public void Tick(float deltaTime)
        {
            ApplyGravity(deltaTime);
            Move(deltaTime);
            Rotate(deltaTime);
        }

        private void Move(float deltaTime)
        {
            var direction = transform.TransformDirection(
                new Vector3(_moveInput.x, 0f, _moveInput.y)
            );

            _controller.Move(direction * (_networkConfig.MoveSpeed * deltaTime));
        }

        private void Rotate(float deltaTime)
        {
            transform.Rotate(
                Vector3.up,
                _rotateInput * _networkConfig.RotationSpeed * deltaTime
            );
        }

        private void ApplyGravity(float deltaTime)
        {
            if (!_controller.isGrounded)
                _controller.Move(Physics.gravity * deltaTime);
        }
    }
}