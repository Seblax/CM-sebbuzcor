using System.Collections.Generic;

namespace StateManagement
{
    public interface IStateMachine
    {
        IState State { get; }
        List<Transition> Transitions { get; }

        abstract void InitializeStateMachine();
        void TransitionToState(IState targetState);
        void HandleStateTransitions();
    }
}