using CodeBase.Infrastructure.StateMachine;

namespace CodeBase.Infrastructure.Services.StateMachine
{
    public class StateMachineProviderService : IStateMachineProviderService
    {
        public IStateMachine StateMachine { get; }

        public StateMachineProviderService(IStateMachine stateMachine) => 
            StateMachine = stateMachine;
    }
}