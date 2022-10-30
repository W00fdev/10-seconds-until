using UnityEngine;

namespace Infrastructure.Services.FactoryService
{
    public interface IProjectileFactoryService<TCreatable> : IFactoryService<TCreatable>
        where TCreatable : class, ICreatable
    {
        public Transform CameraTransform { get; set; }
    }
}
