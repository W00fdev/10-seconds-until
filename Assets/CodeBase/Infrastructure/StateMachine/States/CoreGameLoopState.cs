namespace Infrastructure.GameStateMachine.States
{
    public class CoreGameLoopState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;

        public CoreGameLoopState(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}
