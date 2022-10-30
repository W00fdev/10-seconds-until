using UnityEngine;

namespace Infrastructure.Services.FactoryService
{
    public class FactoryService<TCreatable> : IFactoryService<TCreatable> 
        where TCreatable : class, ICreatable
    {
        protected readonly TCreatable FactoryPrefab;

        public FactoryService(TCreatable prefab)
        {
            FactoryPrefab = prefab;
        }

        public virtual TCreatable Build(Vector3 position, Quaternion rotation, Transform parent = null)
            => GameObject.Instantiate(FactoryPrefab.GameObject, position, rotation, parent) as TCreatable;   
    }
}