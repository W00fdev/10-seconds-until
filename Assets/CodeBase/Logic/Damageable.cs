using Infrastructure.Services.SoundService;
using UnityEngine;

namespace Logic
{
    public class Damageable : MonoBehaviour
    {
        [field: SerializeField]
        public float Damage { get; private set; }

        public ISoundService _soundService;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Healthable healthable))
            {
                _soundService.PlaySoundOfType(SoundType.TRAP);
                healthable.TrappedDamage(Damage);
            }
        }
    }
}
