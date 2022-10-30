using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

namespace Logic
{
    public class Healthable : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _health;

        [SerializeField] private Image _hpBar;

        [SerializeField] private float _canTrappedTime = 1f;

        public UnityEvent OnDied;

        bool _canBeTrapped = true;

        private void Start()
        {
            UpdateHealthbar();
        }

        public void Damage(float damage)
        {
            _health = Mathf.Clamp(_health - damage, 0f, _maxHealth);

            if (_health <= 0f)
                OnDied?.Invoke();

            UpdateHealthbar();
        }

        public void UpdateHealthbar() => _hpBar.fillAmount = _health / _maxHealth;

        public void TrappedDamage(float damage)
        {
            if (_canBeTrapped == false)
                return;

            Damage(damage);

            _canBeTrapped = false;
            StartCoroutine(TrapReload());
        }

        IEnumerator TrapReload()
        {
            yield return new WaitForSeconds(_canTrappedTime);
            _canBeTrapped = true;
        }
    }
}
