using UnityEngine;

namespace Infrastructure.Services.FactoryService
{
    public interface IFactoryService<TCreatable> : IService
        where TCreatable : class, ICreatable
    {
        TCreatable Build(Vector3 position, Quaternion rotation, Transform parent = null);
    }
}
