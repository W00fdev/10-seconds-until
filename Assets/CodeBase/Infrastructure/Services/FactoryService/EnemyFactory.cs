using Logic.Enemies;
using UnityEngine;

namespace Infrastructure.Services.FactoryService
{
    public class EnemyFactory<TEnemy> : FactoryService<TEnemy>, IEnemyFactoryService<TEnemy> 
        where TEnemy : class, IEnemy
    {
        public Transform PlayerTarget { get; set; }

        public EnemyFactory(TEnemy prefab) : base(prefab)
        {
        }
    }
}
