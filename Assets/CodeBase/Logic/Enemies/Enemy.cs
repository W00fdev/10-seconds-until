using Infrastructure.Services.SoundService;
using System.Collections;
using Infrastructure;
using UnityEngine.AI;
using Logic.Managers;
using Logic.Player;
using UnityEngine;
using System;

namespace Logic.Enemies
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        public Rigidbody RigidbodyEnemy;
        public NavMeshAgent NavMeshAgentEnemy;

        public EnemyManager EnemyDirector;

        public Transform PlayerTarget { get; set; }

        public GameObject GameObject => gameObject;

        [SerializeField] private Transform _target;

        [SerializeField] private float _attackDistance;
        [SerializeField] private float _damage;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _cnockbackForce;
        [SerializeField] private float _cnockbackTime;
        [SerializeField] private float _attackReloadTime;

        private ISoundService _soundService;

        private Healthable _playerHealthable;
        private Healthable _healthable;
        private bool _navmeshBased;

        private bool _isAttacking = false;


        private void Awake()
        {
            _healthable = GetComponent<Healthable>();
            _soundService = AllServices.Container.Single<ISoundService>();
        }

        private void Start()
        {
            NavMeshAgentEnemy.updatePosition = false;
            NavMeshAgentControlled(true);
        }

        public void SetNewTarget(Transform newTarget)
        {
            _target = newTarget;
            _playerHealthable = _target.GetComponent<Healthable>();
        }

        private void NavMeshAgentControlled(bool switchOn)
        {
            NavMeshAgentEnemy.updateRotation = switchOn;
            _navmeshBased = switchOn;
        }

        private void FixedUpdate()
        {
            if (_navmeshBased == false)
                return;

            NavMeshAgentEnemy.SetDestination(_target.position);
            Vector3 movementDirection = (_target.position - transform.position).normalized;
            RigidbodyEnemy.MovePosition(transform.position + _movementSpeed * Time.fixedDeltaTime * movementDirection);
            if (CheckDistance() == true && _isAttacking == false)
                StartCoroutine(AttackCoroutine());
        }

        private bool CheckDistance() => Vector3.Distance(_target.position, transform.position) <= _attackDistance;

        IEnumerator AttackCoroutine()
        {
            _isAttacking = true;
            yield return new WaitForSeconds(_attackReloadTime);
            if (CheckDistance())
            {
                _playerHealthable.Damage(_damage);
                _soundService.PlaySoundOfType(SoundType.IMPACT);
            }
            _isAttacking = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out RollableDamage rollable))
                ImpulseCnockBack(rollable);
        }

        public void ExplodeCnockback(float explosionForce, Vector3 position, float explosionRadius, float damage)
        {
            NavMeshAgentControlled(false);

            Vector3 impulseOverCube = (transform.position - position);
            var alphaAngle = Vector3.Angle(position, Vector3.down);
            impulseOverCube.x *= (float)Math.Sin(alphaAngle);
            impulseOverCube.y *= (float)Math.Sin(alphaAngle);

            position.y = transform.position.y;
            impulseOverCube.y = transform.position.y;

            RigidbodyEnemy.AddForce(impulseOverCube * 2f, ForceMode.VelocityChange);
            RigidbodyEnemy.AddExplosionForce(explosionForce, position, explosionRadius, 0.2f, ForceMode.VelocityChange);

            _healthable.Damage(damage);

            StartCoroutine(CnockbackCoroutine());
        }

        private void ImpulseCnockBack(RollableDamage rollable)
        {
            NavMeshAgentControlled(false);

            Vector3 impulseDirection = transform.position - rollable.transform.position;
            impulseDirection.Normalize();

            RigidbodyEnemy.AddForce(impulseDirection * rollable.CnockbackForce, ForceMode.VelocityChange);
            _healthable.Damage(rollable.Damage);
            StartCoroutine(CnockbackCoroutine());
        }

        IEnumerator CnockbackCoroutine()
        {
            yield return new WaitForSeconds(_cnockbackTime);
            NavMeshAgentControlled(true);
        }

        public void DestroyMe()
        {
            EnemyDirector.DelayedRefindEnemies();
            Destroy(gameObject);
        }
    }
}
