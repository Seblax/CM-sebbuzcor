using Gamemanager;
using StateManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using ui;
using UnityEngine;

namespace Minigame
{
    public class MinigameManager : Singleton<MinigameManager>, IStateMachine
    {
        public Minigame minigame;
        public GameManager gameManager;

        public bool isMoving;
        public bool isTittleOver;
        public bool isPlaying;
        public bool isScoreScreenOver;

        //State Machine
        public IState State => _state;
        public IState _state;
        public List<StateManagement.Transition> Transitions => _transitions;
        public List<StateManagement.Transition> _transitions = new List<StateManagement.Transition>();

        public Action Move;
        public Action<bool> Pause;

        public string CurrentState;

        public void Start()
        {
            gameManager = GameManager.instance;
            LoadNewMinigame();
            InitializeStateMachine();
        }

        public virtual void InitializeStateMachine()
        {
            isTittleOver = false;
            isPlaying = false;
            isScoreScreenOver = false;

            // Initialize States
            MinigameInitialState initialState = new();
            MinigameTittleState tittleState = new(minigame);
            MinigamePlayingState playingState = new(minigame);
            MinigameVictoryState winState = new(minigame);
            MinigameDefeatState defeatState = new(minigame);
            MinigameStopState stopState = new();

            _transitions = new List<StateManagement.Transition>
            {
                 new() {
                    Condition = () => (!isMoving) ,
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
                },
                new() {
                    Condition = () => isScoreScreenOver,
                    Source = winState,
                    Target = initialState,
                },
                new() {
                    Condition = () => isScoreScreenOver && GameManager.instance.lives == 0,
                    Source = defeatState,
                    Target = stopState,
                },
                new() {
                    Condition = () => isScoreScreenOver && GameManager.instance.lives > 0,
                    Source = defeatState,
                    Target = initialState,
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
            foreach (StateManagement.Transition transition in _transitions)
            {
                if (transition.Source == _state && transition.Condition())
                {
                    TransitionToState(transition.Target);
                    if (_state.GetType().Name != transition.Target.GetType().Name)
                        Debug.Log($"Transitioning from {_state.GetType().Name} to {transition.Target.GetType().Name}");
                    else
                        Debug.Log($"Staying in {_state.GetType().Name}.");
                    break;
                }
            }
        }

        public void Update()
        {
            _state.OnExecute();
            HandleStateTransitions();

            CurrentState = _state.GetType().Name;
        }

        public void Destroy(GameObject o)
        {
            Destroy(o, 2.5f);
        }

        public void UpdatePauseState(bool shouldPause)
        {
            Pause?.Invoke(shouldPause);
        }

        void LoadNewMinigame()
        {
            // 1. Si ya había un minijuego, destruimos su objeto físico
            if (this.minigame != null && this.minigame.minigameObject != null)
            {
                this.minigame.minigameObject.SetActive(false);
                this.minigame = null;
            }

            gameManager.SetCurrentRound = gameManager.GetCurrentRound + 1;
            Aceleration.IncrementTimeScale();

            // 2. Cargamos el nuevo (esto sobreescribe la variable con datos frescos)
            this.minigame = gameManager.LoadMinigame();
        }
        public IEnumerator EndGame()
        {
            yield return new WaitForSecondsRealtime(5f);


            TransitionManager.instance.SetTransitionPrefab(Resources.Load<GameObject>("Prefabs/UI/Transitions/TransitionMinigame"));
            TransitionManager.instance.TransitionTo("PrincipalMenu");
            
            MinigameUIManager.RemoveGameObject();
            
            GameManager.instance.ResetGameManager();
            RemoveGameObject();
        }
    }

}