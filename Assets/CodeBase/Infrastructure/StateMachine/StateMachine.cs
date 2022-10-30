using Infrastructure.GameStateMachine.States;
using System.Collections.Generic;
using Assets.CodeBase.Logic.UI;
using Logic.Managers;
using System.Linq;
using UnityEngine;
using Logic;

namespace Infrastructure.GameStateMachine
{
    public class StateMachine : IStateSwitcher
    {
        private readonly SoundHandler _soundHandler;
        private readonly SceneLoader _sceneLoader;
        private readonly Curtain _curtain;
        private readonly IProjectile _enemyProjectilePrefab;

        private List<IExitableState> _states;
        private IExitableState _currentState;

        public StateMachine(SoundHandler soundHandler, SceneLoader sceneLoader, Curtain curtain, IProjectile enemyProjectilePrefab)
        {
            _soundHandler = soundHandler;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _enemyProjectilePrefab = enemyProjectilePrefab;

            InitStateMachine();
        }

        private void InitStateMachine()
        {
            _states = new List<IExitableState>()
            {
                new BootstrapperState(this, _soundHandler, _sceneLoader, _enemyProjectilePrefab),
                new LoadLevelState(this, _sceneLoader, _curtain),
                new CoreGameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState newState = SwitchState<TState>();
            newState.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            IPayloadedState<TPayload> newState = SwitchState<TState>();
            newState.Enter(payload);
        }

        private TState SwitchState<TState>() where TState : class, IExitableState
        {
            TState newState = _states.FirstOrDefault((state) => state is TState) as TState;
            
            _currentState?.Exit();
            _currentState = newState;

            return newState;
        }
    }
}
