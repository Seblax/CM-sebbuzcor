using StateManagement;
using System.Collections;
using UnityEngine;

namespace ui
{
    public class MenuTransitionEndState : IState
    {
        MenuTransition transition;
        public MenuTransitionEndState(MenuTransition transition)
        {
            this.transition = transition;
        }

        public void OnEnter()
        {
            this.transition.TransitionRectTransform.anchoredPosition = new Vector2(this.transition.xf, 0);
            this.transition.PlayFadeOut();
        }

        public void OnExecute()
        {

        }

        public void OnExit()
        {

        }
    }

}