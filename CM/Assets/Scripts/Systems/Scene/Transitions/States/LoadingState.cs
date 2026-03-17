using StateManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ui
{

    public class LoadingState : IState
    {
        Transition transition;

        public LoadingState(Transition transition)
        {
            this.transition = transition;
        }

        public void OnEnter()
        {
            this.transition.PlayLoadingScreen();
        }

        public void OnExecute()
        {
        }

        public void OnExit()
        {
        }
    }
}