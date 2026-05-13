using StateManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Game3
{
    public class CatBehaviour : MonoBehaviour, IStateMachine, IPausable
    {
        public Cat cat;
        [SerializeField] private bool InCollisionWihtLight;

        [SerializeField] private bool paused = true;

        public bool IsPaused => paused;

        //State Machine
        public IState _state;
        public IState State { get => _state; set => _state = value; }
        public List<StateManagement.Transition> Transitions => _transitions;


        public List<StateManagement.Transition> _transitions = new List<StateManagement.Transition>();

        private void Start()
        {

            cat = GetComponent<Cat>();
            InitializeStateMachine();
        }

        private void OnEnable()
        {
            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause += SetPaused;
        }

        private void OnDisable()
        {
            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause -= SetPaused;
        }

        private void Update()
        {
            if (IsPaused) return;
            State.OnExecute();
            ((IStateMachine)this).HandleStateTransitions();
        }

        //State Machine
        public virtual void InitializeStateMachine()
        {
            // Initialize States
            InitialCatState initialState = new(cat);
            IdleCatState idleState = new(cat);
            DisturbCatState disturbState = new(cat);
            CatchedCatState catchedState = new(cat);

            _transitions = new List<StateManagement.Transition>
            {
                 new() {
                    Condition = () => (!IsPaused) ,
                    Source = initialState,
                    Target = idleState,
                 },
                 new() {
                    Condition = () => InCollisionWihtLight,
                    Source = idleState,
                    Target = disturbState,
                 },
                new() {
                    Condition = () => !InCollisionWihtLight && !cat.IsCatched,
                    Source = disturbState,
                    Target = idleState,
                },
                new() {
                    Condition = () => cat.IsCatched,
                    Source = disturbState,
                    Target = catchedState,
                },
            };

            _state = initialState;
            _state.OnEnter();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            InCollisionWihtLight = other.gameObject.tag == Data.Minigame.PLAYER_TAG;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            InCollisionWihtLight = other.gameObject.tag == Data.Minigame.PLAYER_TAG ? false : true;
        }

        public void SetPaused(bool isPaused)
        {
            this.paused = isPaused;
        }
    }
}