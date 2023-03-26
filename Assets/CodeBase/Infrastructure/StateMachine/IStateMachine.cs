using CodeBase.Infrastructure.StateMachine.States.Interfaces;

namespace CodeBase.Infrastructure.StateMachine
{
    public interface IStateMachine
    {
        void Enter<TState>() where TState : class, IState;

        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
    }
}