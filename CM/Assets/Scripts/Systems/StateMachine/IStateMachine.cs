using System.Collections.Generic;

namespace StateManagement
{
    public interface IStateMachine
    {
        public IState State { get; set; }
        List<Transition> Transitions { get; }

        abstract void InitializeStateMachine();
        
        public void TransitionToState(IState targetState) {
            State.OnExit();
            State = targetState;
            State.OnEnter();
        }

        public void HandleStateTransitions()
        {
            foreach (StateManagement.Transition transition in Transitions)
            {
                if (transition.Source == State && transition.Condition())
                {
                    TransitionToState(transition.Target);
                    break;
                }
            }
        }
    }
}