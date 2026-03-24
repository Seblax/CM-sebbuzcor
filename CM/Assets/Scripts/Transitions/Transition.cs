using StateManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StateTransition = StateManagement.Transition;

namespace ui
{
    public class Transition : MonoBehaviour, IStateMachine
    {
        public string sceneName = string.Empty;

        protected IState _state;
        protected List<StateTransition> _transitions = new List<StateTransition>();

        public IState State => _state;
        public  List<StateTransition> Transitions => _transitions;

        public bool isSceneLoaded = false;

        public void Play(string sceneName)
        {
            this.sceneName = sceneName;
            this.InitializeStateMachine();
        }

        public virtual void InitializeStateMachine()
        {
            // Initialize States
            StartState startState = new(this);
            LoadingState loadingState = new(this);
            EndState endState = new(this);

            _transitions = new List<StateTransition>
            {
                new() {
                    Condition = () => true,
                    Source = startState,
                    Target = loadingState,
                },
                new() {
                    Condition = () => isSceneLoaded,
                    Source = loadingState,
                    Target = endState,
                }
            };

            _state = startState;
            _state.OnEnter();
        }

        public void TransitionToState(IState targetState)
        {
            _state.OnExit();
            _state = targetState;
            _state.OnEnter();
        }

        public void HandleStateTransitions()
        {
            foreach (StateTransition transition in _transitions)
            {
                if (transition.Source == _state && transition.Condition())
                {
                    TransitionToState(transition.Target);
                    break;
                }
            }
        }

        //###################################

        public void PlayLoadingScreen()
        {
            StopAllCoroutines();
            StartCoroutine(LoadSceneRoutine());
        }

        private IEnumerator LoadSceneRoutine()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(this.sceneName);

            yield return new WaitForSeconds(0.5f);

            while (operation.progress < 0.9f)
            {
                yield return null;
            }

            operation.allowSceneActivation = true;
            this.isSceneLoaded = true;
        }
    }
}