namespace Infrastructure.GameStateMachine
{
    public interface IStateSwitcher
    {
        void Enter<TState>() where TState : class, IState;

        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}
