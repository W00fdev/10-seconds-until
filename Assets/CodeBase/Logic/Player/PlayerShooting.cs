using Infrastructure.Services.FactoryService;
using Infrastructure.Services.InputService;
using Infrastructure.Services.SoundService;
using System.Collections;
using Infrastructure;
using UnityEngine;

namespace Logic.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private float _fireRateSeconds = 0.5f;

        private IProjectileFactoryService<IProjectile> _projectileFactory;

        private IInputService _inputService;
        private ISoundService _soundService;

        private Camera _camera;
        private Ray _shootingRay;

        private bool _fireReady;

        private void Awake()
        {
            InitServices();

            _camera = Camera.main;
            _fireReady = true;
        }

        private void FixedUpdate()
        {
            ProcessFire();
        }

        private void InitServices()
        {
            _soundService = AllServices.Container.Single<ISoundService>();
            _inputService = AllServices.Container.Single<IInputService>();

            _projectileFactory = new ProjectileFactory<IProjectile>(_projectilePrefab);
            _projectileFactory.CameraTransform = Camera.main.transform;
        }

        private void ProcessFire()
        {
            if (_inputService.GetFireInput() == false)
                return;

            if (_fireReady == false)
                return;

            _shootingRay = _camera.ScreenPointToRay(_inputService.MousePosition);
            if (Physics.Raycast(_shootingRay, out RaycastHit hit))
            {
                Vector3 bubblePosition = _camera.transform.position + _camera.transform.forward;
                Vector3 bubbleDirection = (hit.point - _camera.transform.position).normalized;

                _projectileFactory.Build(bubblePosition, Quaternion.LookRotation(bubbleDirection));
                _soundService.PlaySoundOfType(SoundType.SHOOT);
            }

            StartCoroutine(ShootingFirerate());
        }

        IEnumerator ShootingFirerate()
        {
            _fireReady = false;
            yield return new WaitForSeconds(_fireRateSeconds);
            _fireReady = true;
        }
    }
}
