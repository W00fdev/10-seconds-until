using Infrastructure.Services.FactoryService;
using UnityEngine;

namespace Logic
{
    public interface IProjectile : ICreatable
    {
        float Damage { get; set; }
        float Speed { get; set; }
    }
}

