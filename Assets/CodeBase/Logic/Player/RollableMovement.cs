using Infrastructure.Services.InputService;
using Infrastructure.Services.SoundService;
using Infrastructure;
using UnityEngine;
using System.Collections;

namespace Logic.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class RollableMovement : MonoBehaviour
    {
        public Rigidbody RigidbodyPlayer;

        [SerializeField] private LayerMask _layerMaskJumpable;

        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpTorqueVelocity;
        [SerializeField] private float _accelerationAdded;
        [SerializeField] private float _deaccelerationAdded;
        [SerializeField] private float _accelerationInAirCoeff;
        [SerializeField] private float _maxAcceleration;
        [SerializeField] private float _groundCheckDistance;

        private Ray _rayGroundCheck = new Ray(Vector3.zero, Vector3.down);
        private Vector3 _inputDirection;
        private Vector3 _movementVector;
        private int _jumpTorqueDirection = 1;
        private float _acceleration;
        private bool _isGrounded;
        private bool _jumpReload;

        private IInputService _inputService;
        private ISoundService _soundService;

        private void Awake()
        {
            _soundService = AllServices.Container.Single<ISoundService>();
            _inputService = AllServices.Container.Single<IInputService>();

            UpdateRayOrigin();
        }

        private void FixedUpdate()
        {
            _inputDirection = _inputService.GetMovementDirection();
            _movementVector = TranslateCameraDirection(_inputDirection);
        
            GroundCheck();
            ProcessJump();
            MovePlayerTowards(_movementVector);
        }

        public void MovePlayerTowards(Vector3 inputDirection)
        {
            if (inputDirection == Vector3.zero)
            {
                if (_isGrounded)
                    _acceleration = Mathf.Lerp(_acceleration, 0f, Time.fixedDeltaTime);

                return;
            }

            RecalculateAcceleration(inputDirection);
            AddAirTorque(inputDirection);

            Vector3 velocityVector = inputDirection * Mathf.Abs(_acceleration);
            RigidbodyPlayer.AddForce(velocityVector, ForceMode.Acceleration);
        }

        private void AddAirTorque(Vector3 inputDirection)
        {
            if (_isGrounded != false)
                return;
            
            Vector3 airTorque = new Vector3(inputDirection.z, 0f, -inputDirection.x) * _acceleration;
            RigidbodyPlayer.AddTorque(airTorque * _accelerationInAirCoeff, ForceMode.Acceleration);
        }

        private void AddJumpTorque()
        {
            Vector3 jumpTorque = _jumpTorqueVelocity * _jumpTorqueDirection * Vector3.up;
            if (_jumpTorqueDirection < 0f)
                jumpTorque *= 1.5f;

            RigidbodyPlayer.AddTorque(jumpTorque, ForceMode.VelocityChange);
            _jumpTorqueDirection *= -1;
        }

        private void ProcessJump()
        {
            if (_isGrounded == false || _jumpReload == true)
                return;

            if (_inputService.GetJumpInput() == true)
            {
                _soundService.PlaySoundOfType(SoundType.JUMP);

                PlayerJump();
                AddJumpTorque();

                StartCoroutine(JumpReload());
            }
        }

        private void PlayerJump() 
            => RigidbodyPlayer.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);

        private void GroundCheck()
        {
            UpdateRayOrigin();
            
            _isGrounded = Physics.Raycast(_rayGroundCheck, _groundCheckDistance, _layerMaskJumpable);
        }

        private void RecalculateAcceleration(Vector3 inputDirection)
        {
            float frameAcceleration = _deaccelerationAdded * Time.fixedDeltaTime;

            if (inputDirection != Vector3.zero && Vector3.Angle(RigidbodyPlayer.velocity, inputDirection) > 90f)
                frameAcceleration *= -1f;

            if (_isGrounded == false)
                frameAcceleration *= _accelerationInAirCoeff;

            _acceleration += frameAcceleration;
            _acceleration = Mathf.Clamp(_acceleration, 0f, _maxAcceleration);
        }

        private Vector3 TranslateCameraDirection(Vector3 inputDirection)
        {
            Vector3 movementVector = Vector3.zero;

            if (inputDirection.sqrMagnitude > Constants.Epsilon)
            {
                //Трансформируем экранныые координаты вектора в мировые
                movementVector = Camera.main.transform.TransformDirection(inputDirection);
                movementVector.y = 0;
            }

            return movementVector;
        }

        private void UpdateRayOrigin() => _rayGroundCheck = new Ray(transform.position, Vector3.down);

        IEnumerator JumpReload()
        {
            _jumpReload = true;
            yield return new WaitForSeconds(0.3f);
            _jumpReload = false;
        }
    }
}
