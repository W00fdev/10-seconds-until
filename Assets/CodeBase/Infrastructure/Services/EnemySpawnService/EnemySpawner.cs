using Infrastructure.Services.FactoryService;
using System.Collections;
using Logic.Enemies;
using UnityEngine;

namespace Infrastructure.Services.EnemySpawnService
{
    public class EnemySpawner<TEnemy> where TEnemy : class, IEnemy
    {
        public Transform PlayerTarget;

        private readonly TEnemy _spawnedUnitPrefab;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly EnemySpawnerData _spawnerData;

        private readonly IEnemyFactoryService<TEnemy> _enemyFactory;

        private Coroutine _spawnerCoroutine;

        public EnemySpawner(TEnemy spawnedUnitPrefab, ICoroutineRunner coroutineRunner, EnemySpawnerData spawnerData)
        {
            _spawnedUnitPrefab = spawnedUnitPrefab;
            _coroutineRunner = coroutineRunner;
            _spawnerData = spawnerData;

            _enemyFactory = new EnemyFactory<TEnemy>(_spawnedUnitPrefab);
        }

        public void StartSpawner() => _spawnerCoroutine = _coroutineRunner.StartCoroutine(SpawnerCoroutine());

        public void StopSpawner() => _coroutineRunner.StopCoroutine(_spawnerCoroutine);

        IEnumerator SpawnerCoroutine()
        {
            WaitForSeconds waitingTime = new WaitForSeconds(_spawnerData.TickTime);
            for(int ticks = 0; ticks < _spawnerData.TicksCount; ticks++) 
            {
                yield return waitingTime;

                for (int spawnedCount = 0; spawnedCount < _spawnerData.UnitsPerTick; spawnedCount++)
                {
                    TEnemy spawnedEnemy = _enemyFactory.Build(_spawnerData.SpawnPoint.position, Quaternion.identity, _spawnerData.Parent);
                    spawnedEnemy.PlayerTarget = PlayerTarget;
                }
            }
        }
    }
}
