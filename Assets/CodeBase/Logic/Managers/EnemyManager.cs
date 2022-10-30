using Infrastructure.Services.SoundService;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using Infrastructure;
using Logic.Enemies;
using UnityEngine;
using System.Linq;

namespace Logic.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        public UnityEvent OnAllDied;

        [field: SerializeField]
        public Transform EnemiesTarget { get; private set; }
        public Transform EnemyParent;
        public List<Enemy> Enemies = new List<Enemy>();

        private ISoundService _soundService;

        private void Awake()
        {
            _soundService = AllServices.Container.Single<ISoundService>();
            RefindEnemies();
        }

        public void SwitchTargetForEnemies(Transform newTarget)
        {
            EnemiesTarget = newTarget;
            foreach (Enemy enemy in Enemies)
                enemy.SetNewTarget(EnemiesTarget);
        }

        public void DelayedRefindEnemies()
        {
            _soundService.PlaySoundOfType(SoundType.DEAD);
            StartCoroutine(RefindDelayed());
        }

        IEnumerator RefindDelayed()
        {
            yield return null;
            yield return Time.deltaTime;

            RefindEnemies();
        }

        public void RefindEnemies()
        {
            Enemies.Clear();

            foreach (Enemy Enemy in EnemyParent.GetComponentsInChildren(typeof(Enemy)).Cast<Enemy>())
            {
                Enemy.EnemyDirector = this;
                Enemy.SetNewTarget(EnemiesTarget);
                Enemies.Add(Enemy);
            }

            if (Enemies.Count <= 0)
                OnAllDied?.Invoke();
        }
    }

}
