using UnityEngine;
using Logic;

namespace Infrastructure.Services.FactoryService
{
    public class ProjectileFactory<TProjectile> : FactoryService<TProjectile>, IProjectileFactoryService<TProjectile>
    where TProjectile : class, IProjectile
    {
        public Transform CameraTransform { get; set; }

        public ProjectileFactory(TProjectile prefab) : base(prefab)
        {
        }

/*        public override TProjectile Build(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            TProjectile projectile = GameObject.Instantiate(FactoryPrefab.GameObject, position, rotation, parent) as TProjectile;
            return projectile;
        }*/
    }
}
