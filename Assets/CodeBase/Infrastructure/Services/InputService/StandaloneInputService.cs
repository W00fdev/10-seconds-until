using UnityEngine;

namespace Infrastructure.Services.InputService
{
    public class StandaloneInputService : IInputService
    {
        private static Vector3 _movementDirection = Vector3.zero;
        private static Vector3 _mouseMovement = Vector3.zero;

        public Vector3 MousePosition => Input.mousePosition;

        public Vector3 GetMovementDirection()
        {
            _movementDirection.x = Input.GetAxis("Horizontal");
            _movementDirection.z = Input.GetAxis("Vertical");

            _movementDirection.Normalize();

            return _movementDirection;
        }

        public Vector3 GetMouseMovement()
        {
            _mouseMovement.x = Input.GetAxis("Mouse Y");
            _mouseMovement.y = Input.GetAxis("Mouse X");

            return _mouseMovement;
        }

        public bool GetJumpInput() => Input.GetAxisRaw("Jump") != 0f;

        public bool GetFireInput() => Input.GetAxisRaw("Fire1") != 0f;

        public bool GetHookInput() => Input.GetAxisRaw("Fire2") != 0f;
    }
}

