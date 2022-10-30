using Infrastructure.Services.FactoryService;
using UnityEngine;

namespace Logic.Enemies
{
    public interface IEnemy : ICreatable
    {
        Transform PlayerTarget { get; set; }
    }
}
