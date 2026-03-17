using StateManagement;

namespace ui
{

    public class StartState : IState
    {
        Transition transition;

        public StartState(Transition transition)
        {
            this.transition = transition;
        }

        public void OnEnter()
        {
            this.transition.isSceneLoaded = false;
        }

        public void OnExecute()
        {
        }

        public void OnExit()
        {
        }
    }
}