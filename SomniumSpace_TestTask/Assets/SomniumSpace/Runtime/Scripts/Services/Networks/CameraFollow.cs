using UnityEngine;

namespace SomniumSpace.Services.Networks
{
    public class CameraFollow : MonoBehaviour
    {
        private const float FOLLOW_SPEED = 10f;
        private const float ROTATE_SPEED = 10f;
        private static readonly Vector3 Offset = new(0f, 2.5f, -5f);

        private Transform _target;

        public void SetTarget(Transform target) => _target = target;

        private void LateUpdate()
        {
            if (_target == null) return;

            var desiredPosition = _target.TransformPoint(Offset);
            var desiredRotation = Quaternion.LookRotation(_target.position - desiredPosition);

            transform.position = Vector3.Lerp(transform.position, desiredPosition, FOLLOW_SPEED * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, ROTATE_SPEED * Time.deltaTime);
        }
    }
}