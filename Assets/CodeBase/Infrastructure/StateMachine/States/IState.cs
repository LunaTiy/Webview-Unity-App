﻿namespace CodeBase.Infrastructure.StateMachine.States
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}