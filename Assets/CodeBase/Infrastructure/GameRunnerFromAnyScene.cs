using UnityEngine;

namespace Infrastructure
{
    public class GameRunnerFromAnyScene : MonoBehaviour
    {
        [SerializeField] private Bootstrapper _bootstrapperPrefab;

        private void Awake()
        {
            Bootstrapper bootstrapper = FindObjectOfType<Bootstrapper>();

            if (bootstrapper == null)
                Instantiate(_bootstrapperPrefab);
        }
    }
}
