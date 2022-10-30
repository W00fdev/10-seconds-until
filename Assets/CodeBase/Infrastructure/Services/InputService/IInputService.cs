using UnityEngine;

namespace Infrastructure.Services.InputService
{
    public interface IInputService : IService
    {
        public Vector3 MousePosition { get; }

        Vector3 GetMovementDirection();
        Vector3 GetMouseMovement();

        bool GetFireInput();
        bool GetHookInput();

        bool GetJumpInput();
    }

}