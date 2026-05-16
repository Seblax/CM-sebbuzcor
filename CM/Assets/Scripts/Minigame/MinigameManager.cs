using Gamemanager;
using StateManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minigame
{
    public class MinigameManager : Singleton<MinigameManager>, IStateMachine
    {
        public Minigame minigame;
        public GameManager gameManager;

        public bool isMoving => mover != null && mover.isMoving;
        public bool isTittleOver;
        public bool isPlaying;
        public bool isScoreScreenOver;

        //State Machine
        public IState _state;
        public List<StateManagement.Transition> Transitions => _transitions;
        public IState State { get => _state; set => _state = value; }
        public List<StateManagement.Transition> _transitions = new List<StateManagement.Transition>();

        [SerializeField] public string CurrentState => _state.GetType().Name;


        //Mover Animation
        MinigameMover mover;
        public MinigameMover Mover { get => mover; }
        public MinigameMover SetMover { set => mover = value; }

        //Pause
        public bool IsPaused;
        public Action<bool> Pause;


        public void Start()
        {
            if (GameManager.instance.isGameOver) Destroy(this.gameObject);

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
            MinigameInitialState initialState = new(minigame);
            MinigameTittleState tittleState = new(minigame);
            MinigamePlayingState playingState = new(minigame);
            MinigameVictoryState winState = new(minigame);
            MinigameDefeatState defeatState = new(minigame);
            MinigameEndingState endState = new(minigame);

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
                    Condition = () => !isPlaying && !isMoving && minigame.Lose && GameManager.instance.lives > 1,
                    Source = playingState,
                    Target = defeatState,
                },
                new() {
                    Condition = () => !isPlaying && !isMoving && minigame.Lose && GameManager.instance.lives <= 1,
                    Source = playingState,
                    Target = endState,
                },
                new() {
                    Condition = () => isScoreScreenOver  && !isMoving,
                    Source = winState,
                    Target = initialState,
                },
                new() {
                    Condition = () => isScoreScreenOver && GameManager.instance.lives > 0 && !isMoving,
                    Source = defeatState,
                    Target = initialState,
                }
            };

            _state = initialState;
            _state.OnEnter();
        }

        public void Update()
        {
            if (GameManager.instance.isGameOver) Destroy(this.gameObject);

            _state.OnExecute();
            ((IStateMachine)this).HandleStateTransitions();
        }

        public void DestroyGameObject(GameObject o)
        {
            if (o != null)
            {
                Destroy(o);
            }
        }

        public void DestroyGameObject(GameObject o, float delay)
        {
            if (o != null)
            {
                Destroy(o, delay);
            }
        }

        public void UpdatePauseState(bool shouldPause)
        {
            IsPaused = shouldPause;
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
            GameObject minigameMusic = GameObject.Find("MinigameMusic");
            
            if (minigameMusic != null)
            {
                Destroy(minigameMusic);
            }

            yield return new WaitForSecondsRealtime(3.5f);

            TransitionManager.instance.SetTransitionPrefab(Resources.Load<GameObject>("Prefabs/UI/Transitions/TransitionMinigame"));
            TransitionManager.instance.TransitionTo("PrincipalMenu");

            GameManager.instance.ResetGameManager();
        }
    }

}