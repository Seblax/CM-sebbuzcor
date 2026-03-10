using UnityEngine;

namespace ui
{
    public class Transitionmanager : Singleton<Transitionmanager>
    {
        private Transition _transition;

        private void Awake()
        {
            DontDestroyOnLoad(instance);
        }

        public void PlayTransition(Transition transition) { 
            this._transition = transition;
            _transition.PlayTransitionIn();
        }

    }
}
