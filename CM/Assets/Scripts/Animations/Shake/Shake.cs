using StateManagement;
using System.Collections.Generic;
using ui;
using UnityEngine;
using StateTransition = StateManagement.Transition;

namespace ShakeAnimation
{

    public class Shake : MonoBehaviour, IStateMachine
    {
        [Header("Configuration")]
        float _speed;
        float _interval;
        float _duration = 0.25f;

        public bool play;
        public bool isPlaying;
        public Vector3 startPosition;

        //State Machine
        private IState _state;
        public IState State { get => _state; set => _state = value; }
        private List<StateTransition> _transitions = new List<StateTransition>();
        public List<StateTransition> Transitions => this._transitions;
        public float Interval => this._interval;
        public float Speed => this._speed;

        public float Duration
        {
            get => this._duration;
            set => this._duration = value;
        }

        void Start()
        {
            this.startPosition = this.transform.localPosition;
            InitializeStateMachine();
        }

        void Update()
        {
            if (_state == null) return;

            _state.OnExecute();
            ((IStateMachine)this).HandleStateTransitions();
        }

        public void Play()
        {
            Play(1f, 1f, 0.25f);
        }


        public void Play(float speed)
        {
            Play(speed, 1f, 0.25f);
        }

        public void Play(float speed, float interval)
        {
            Play(speed, interval, 0.25f);
        }

        public void Play(float speed, float interval, float duration)
        {
            RestartStateMachine();
            this._speed = speed;
            this._interval = interval;
            this._duration = duration;
            this.play = true;
        }

        public virtual void InitializeStateMachine()
        {
            // Initialize States
            ShakeStart startState = new(this);
            ShakeStop stopState = new(this);

            _transitions = new List<StateTransition>
            {
                new() {
                    Condition = () => play,
                    Source = stopState,
                    Target = startState,
                },
                new() {
                    Condition = () => !isPlaying ,
                    Source = startState,
                    Target = stopState,
                }
            };

            _state = stopState;
            _state.OnEnter();
        }

        void RestartStateMachine()
        {
            play = false;
            isPlaying = false;
            InitializeStateMachine();
        }

        public void Stop()
        {
            RestartStateMachine();
            this.transform.localPosition = startPosition;
        }
    }
}