using Infrastructure.GameStateMachine.States;
using Infrastructure.GameStateMachine;
using Assets.CodeBase.Logic.UI;
using Logic.Managers;
using UnityEngine;
using Logic;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private SoundHandler _soundHandlerPrefab;
        [SerializeField] private Curtain _curtainPrefab;
        [SerializeField] private IProjectile _enemyProjectilePrefab;

        private StateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new StateMachine(Instantiate(_soundHandlerPrefab), new SceneLoader(this), 
                Instantiate(_curtainPrefab), _enemyProjectilePrefab);

            _stateMachine.Enter<BootstrapperState>();

            DontDestroyOnLoad(gameObject);
        }
    }
}
