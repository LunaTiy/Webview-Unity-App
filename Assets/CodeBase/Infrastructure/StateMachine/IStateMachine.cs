using CodeBase.Infrastructure.StateMachine.States;

namespace CodeBase.Infrastructure.StateMachine
{
    public interface IStateMachine
    {
        void Enter<T>() where T : IState;
    }
}