using StateManagement;
using System.Collections;
using UnityEngine;

namespace ui
{
    public class MenuTransitionStartState : IState
    {
        MenuTransition transition;
        public MenuTransitionStartState(MenuTransition transition)
        {
            this.transition = transition;
        }

        public void OnEnter()
        {
            this.transition.TransitionRectTransform.anchoredPosition = new Vector2(this.transition.xi, 0);
            this.transition.PlayFadeIn();
        }

        public void OnExecute()
        {
        }

        public void OnExit()
        {
        }
    }
}