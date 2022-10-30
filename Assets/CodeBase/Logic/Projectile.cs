using UnityEngine;

namespace Logic
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        [field: SerializeField]
        public float Damage { get; set; }
        
        [field: SerializeField]
        public float Speed { get; set; }

        public GameObject GameObject { get => gameObject; }

        private Vector3 _direction;

        private void Awake()
        {
            _direction = transform.forward;
        }

        private void FixedUpdate()
        {
            transform.position += Speed * Time.deltaTime * _direction;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Healthable healthable))
                healthable.Damage(Damage);

            Destroy(gameObject);
        }
    }
}

