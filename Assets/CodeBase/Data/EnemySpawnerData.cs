using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EnemySpawnerData", menuName = "ScriptableData")]
    public class EnemySpawnerData : ScriptableObject
    {
        public Transform Parent;
        public Transform SpawnPoint;

        public int UnitsPerTick;
        
        public int TicksCount;
        public float TickTime;
    }
}
