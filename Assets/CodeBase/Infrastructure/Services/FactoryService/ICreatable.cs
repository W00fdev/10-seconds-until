using UnityEngine;

namespace Infrastructure.Services.FactoryService
{
    public interface ICreatable
    {
        public GameObject GameObject { get; }
    }
}