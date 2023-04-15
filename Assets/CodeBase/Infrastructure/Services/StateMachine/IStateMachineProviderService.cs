using CodeBase.Infrastructure.StateMachine;

namespace CodeBase.Infrastructure.Services.StateMachine
{
    public interface IStateMachineProviderService : IService
    {
        IStateMachine StateMachine { get; }
    }
}