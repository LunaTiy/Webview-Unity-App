namespace CodeBase.Infrastructure.StateMachine.States.Interfaces
{
    public interface IPayloadState<in TPayload> : IExitableState
    {
        void Enter(TPayload nextScene);
    }
}