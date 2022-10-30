using Infrastructure.Services.InputService;
using Infrastructure;
using UnityEngine;

namespace Logic.Player
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform Target;

        public float RotationAngleX;
        public float RotationAngleY;
        public float MaxRotationAngleX;

        public LayerMask WallsToAvoidMask;
        public float SafeZoneDistance;

        public int Distance;
        public float OffsetY;

        private IInputService _inputService;

        private void Awake() => _inputService = AllServices.Container.Single<IInputService>();

        private void Start() => Cursor.lockState = CursorLockMode.Locked;

        private void LateUpdate()
        {
            if (Target == null)
                return;

            RotateAroundTarget();
            FollowTarget();
        }

        public void SwitchTarget(Transform newTarget) => Target = newTarget;

        private void RotateAroundTarget()
        {
            Vector3 mouseVector = _inputService.GetMouseMovement();
            RotationAngleX = Mathf.Clamp(RotationAngleX - mouseVector.x, -MaxRotationAngleX, MaxRotationAngleX);
            RotationAngleY += mouseVector.y;
        }

        private void FollowTarget()
        {
            Quaternion rotation = Quaternion.Euler(RotationAngleX, RotationAngleY, 0);
            Vector3 position = rotation * new Vector3(0, 0, -Distance) + FollowingPointPosition();

            StopCameraByWalls(ref position, rotation);

            transform.SetPositionAndRotation(position, rotation);
        }

        private void StopCameraByWalls(ref Vector3 decidedPosition, Quaternion rotation)
        {
            Ray playerToCameraRay = new Ray(Target.position, (decidedPosition - Target.position).normalized);
            if (Physics.Raycast(playerToCameraRay, out RaycastHit hit, Distance + SafeZoneDistance, WallsToAvoidMask))
            {
                float distanceToWall = Vector3.Distance(Target.position, hit.point);
                decidedPosition = rotation * new Vector3(0, 0, -distanceToWall + SafeZoneDistance) + FollowingPointPosition();
            }
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = Target.position;
            followingPosition.y += OffsetY;
            return followingPosition;
        }
    }
}

