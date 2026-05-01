using Gamemanager;
using StateManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Minigame
{
    public class MinigameManager : Singleton<MinigameManager>, IStateMachine
    {
        public Minigame minigame;

        public bool isMoving;
        public bool isTittleOver;
        public bool isStarting;
        public bool isPlaying;

        //State Machine
        public IState State => _state;
        public IState _state;
        public List<Transition> Transitions => _transitions;
        public List<Transition> _transitions = new List<Transition>();

        public Action Move;
        public Action<bool> Pause;

        public GameObject _titleObject;
        public GameObject _minigameObject;
        public GameObject _scoreObject;

        private void Awake()
        {
            this.minigame = Minigame.of(0);

            InitializeStateMachine();
        }

        public virtual void InitializeStateMachine()
        {
            // Initialize States
            MinigameInitialState initialState = new();
            MinigameTittleState tittleState = new(minigame);
            MinigamePlayingState playingState = new(minigame);
            MinigameVictoryState winState = new(minigame);
            MinigameDefeatState defeatState = new(minigame);

            _transitions = new List<Transition>
            {
                 new() {
                    Condition = () => !isStarting,
                    Source = initialState,
                    Target = tittleState,
                },
                new() {
                    Condition = () => !isMoving && isTittleOver,
                    Source = tittleState,
                    Target = playingState,
                },
                new() {
                    Condition = () => !isPlaying && !isMoving && minigame.Win,
                    Source = playingState,
                    Target = winState,
                },
                new() {
                    Condition = () => !isPlaying && !isMoving && minigame.Lose,
                    Source = playingState,
                    Target = defeatState,
                }
            };

            _state = initialState;
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
            //MinigamePlayingState forcePlay = new(this.minigame);
            //_state = forcePlay;
            _state.OnExecute();
            HandleStateTransitions();
        }

        public void Destroy(GameObject o)
        {
            Destroy(o, 2.5f);
        }


        public void UpdatePauseState(bool shouldPause)
        {
            Pause?.Invoke(shouldPause);
        }
    }
}