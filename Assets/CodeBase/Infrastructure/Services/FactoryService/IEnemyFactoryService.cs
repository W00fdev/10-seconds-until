using UnityEngine;

namespace Infrastructure.Services.FactoryService
{
    public interface IEnemyFactoryService<TCreatable> : IFactoryService<TCreatable>
        where TCreatable : class, ICreatable
    {
        public Transform PlayerTarget { get; set; }
    }
}
