using Gamemanager;
using StateManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Minigame
{
    public class MinigameManager : Singleton<MinigameManager>, IStateMachine
    {
        Minigame _minigame;
        public Action<float> UpdateUI;
        public UnityEvent placeHolder;

        //State Machine
        public IState State => _state;
        public IState _state;
        public List<Transition> Transitions => _transitions;
        public List<Transition> _transitions = new List<Transition>();

        private void Awake()
        {
            this._minigame = Minigame.of(7f);
            InitializeStateMachine();
        }

        public virtual void InitializeStateMachine()
        {
            // Initialize States
            MinigameStartState startState = new(_minigame);
            MinigamePauseState stopState = new(_minigame);
            MinigamePlayingState playingState = new(_minigame);

            _transitions = new List<Transition>
            {
                new() {
                    Condition = () => true,
                    Source = startState,
                    Target = playingState,
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
            foreach (Transition transition in _transitions)
            {
                if (transition.Source == _state && transition.Condition())
                {
                    TransitionToState(transition.Target);
                    break;
                }
            }
        }

        public void Update()
        {
            if (this.UpdateUI != null) UpdateUI?.Invoke(_minigame.TimerPercent);
            if (_minigame.IsTimerOver)
            {
                /////////////////////// PLACEHODLER
                this._minigame.Reset();
                placeHolder.Invoke();
                Aceleration.SetScale = Aceleration.GetScale + 0.25f;
                Debug.Log($"Actual Aceleration Scale: {Aceleration.GetScale}");

                MiniGameUiManager.instance.UpdateUI();
            }
        }
    }
}