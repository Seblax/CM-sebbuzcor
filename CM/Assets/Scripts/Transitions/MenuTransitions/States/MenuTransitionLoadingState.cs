using StateManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ui
{
    public class MenuTransitionLoadingState : IState
    {
        MenuTransition transition;
        public MenuTransitionLoadingState(MenuTransition transition)
        {
            this.transition = transition;
        }
        public void OnEnter()
        {
            this.transition.PlayloadingScreen();
        }

        public void OnExecute()
        {
        }

        public void OnExit()
        {
            this.transition.isSceneLoaded = false;
            this.transition.loading = false;
        }
    }
}