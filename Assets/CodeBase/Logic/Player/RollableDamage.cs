using UnityEngine;

namespace Logic.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class RollableDamage : MonoBehaviour
    {
        public Rigidbody RigidbodyPlayer;

        [SerializeField] private float _speedToAttack;
        [SerializeField] private float _cnockbackForce;
        [SerializeField] private float _damage;

        public float Damage { get => _damage; private set => _damage = value; }
        public float CnockbackForce { get => _cnockbackForce; private set => _cnockbackForce = value; }
        
        public bool CanDealDamage
            => (RigidbodyPlayer.velocity.magnitude >= _speedToAttack);
    }
}
