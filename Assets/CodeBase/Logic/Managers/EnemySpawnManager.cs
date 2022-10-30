using Infrastructure;
using Logic.Enemies;
using Logic.Player;
using System.Collections;
using UnityEngine;

namespace Logic.Managers
{
    public class EnemySpawnManager : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private IEnemy _enemyPrefab;
        [SerializeField] private EnemySpawnerData _enemySpawnerData;

        [SerializeField] private EnemyManager _enemyDirector;

        private EnemySpawner<IEnemy> _enemySpawner;
        private IPlayer _player;

        private const string PlayerTag = "Player";

        IEnumerator Start()
        {
            yield return null;
            FindPlayer();
            Debug.Log(_player);

            _enemySpawner = new EnemySpawner<IEnemy>(_enemyPrefab, this, _enemySpawnerData);
            _enemySpawner.PlayerTarget = _player.PlayerTransform;
        }

        public void SpawnerEnable() => _enemySpawner.StartSpawner();

        public void SpawnerDisable() => _enemySpawner.StopSpawner();

        private void FindPlayer()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag(PlayerTag);
            foreach (GameObject player in players)
            {
                if (player.TryGetComponent(out PlayerFormChanger playerForm))
                {
                    _player = playerForm;
                    return;
                }
            }
        }
    }
}
