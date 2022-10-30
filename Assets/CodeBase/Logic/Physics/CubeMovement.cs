using Infrastructure;
using Infrastructure.Services.InputService;
using Infrastructure.Services.SoundService;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UNUSED
/// </summary>
/// 
/*public class CubeMovement : MonoBehaviour
{
    public Rigidbody RigidbodyPlayer;

    private IInputService _inputService;

    [SerializeField] private Transform _camera;

    [SerializeField] private float _speed;
    [SerializeField] private float _speedInAirCoeff;

    [SerializeField] private Vector3 _groundCheckVolume;

    [SerializeField] private float _jumpForce;



    [Header("SLAM Params")]
    [SerializeField] private Vector3 _slamVolume;
    [SerializeField] private float _slamDamage;
    [SerializeField] private float _slamForce;


    private Ray _rayGroundCheck = new Ray(Vector3.zero, Vector3.down);
    private float _acceleration;
    private bool _isGrounded;

    [SerializeField] private LayerMask _layerMaskJumpable;
    [SerializeField] private LayerMask _layerMaskEnemy;

    private ISoundService _soundService;

    private void Awake()
    {
        _soundService = AllServices.Container.Single<ISoundService>();
        _inputService = AllServices.Container.Single<IInputService>();

        UpdateRayOrigin();
    }


    private void FixedUpdate()
    {
        Vector3 inputDirection = _inputService.GetMovementDirection();
        Vector3 movementVector = TranslateCameraDirection(inputDirection);

        _isGrounded = IsGrounded();

        ProcessJump();
        MovePlayerTowards(movementVector);
    }

    private void ProcessJump()
    {
        if (_isGrounded == false)
            return;

        bool jumpInput = _inputService.GetJumpInput();
        if (jumpInput == true)
        {
            _soundService.PlaySoundOfType(SoundType.JUMP);
            PlayerJump();
        }
    }

    private void PlayerJump() => RigidbodyPlayer.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);

    private bool IsGrounded()
    {
        bool isGrounded = false;
        UpdateRayOrigin();


        //if (Physics.Raycast(_rayGroundCheck, _groundCheckDistance, _layerMaskJumpable))
        if (Physics.BoxCast(transform.position + Vector3.up, _groundCheckVolume, Vector3.down, Quaternion.identity, 1.3f, _layerMaskJumpable))
        {
            isGrounded = true;

            // If it wasn't grounded prev frame
            if (_isGrounded == false)
                CastSlam();
        }

        return isGrounded;
    }

    private void CastSlam()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + Vector3.up, _slamVolume, Vector3.down,
            Quaternion.identity, 100f, _layerMaskEnemy);

        foreach (RaycastHit hit in hits)
        {
            Debug.Log(hit);
            if (hit.collider.TryGetComponent(out Rigidbody rigidbody))
            {
                if (hit.collider.TryGetComponent(out Enemy enemy))
                    enemy.ExplodeCnockback(_slamForce, transform.position, _slamVolume.x, _slamDamage);
            }
        }


    }

    private void MovePlayerTowards(Vector3 inputDirection)
    {
        if (inputDirection == Vector3.zero)
        {
            if (_isGrounded)
                _acceleration = Mathf.Lerp(_acceleration, 0f, Time.fixedDeltaTime);

            return;
        }

        float totalSpeed = _speed;
        if (_isGrounded == false)
            totalSpeed  *= _speedInAirCoeff;

        Vector3 newPosition = transform.position + (inputDirection * totalSpeed) * Time.fixedDeltaTime;
        RigidbodyPlayer.MovePosition(newPosition);
    }

    private Vector3 TranslateCameraDirection(Vector3 inputDirection)
    {
        Vector3 movementVector = Vector3.zero;

        if (inputDirection.sqrMagnitude > Constants.Epsilon)
        {
            //Трансформируем экранныые координаты вектора в мировые
            movementVector = _camera.transform.TransformDirection(inputDirection);
            movementVector.y = 0;

            transform.forward = movementVector;
        }

        return movementVector;
    }


    private void UpdateRayOrigin() => _rayGroundCheck = new Ray(transform.position, Vector3.down);
}
*/