using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StateTransition = StateManagement.Transition;


namespace ui
{
    public class MenuTransition : Transition
    {
        [Header("Transition Settings")]
        public float xi = -2500;
        public float xf = 2500;
        public float duration = 0.25f;
        public bool loading;

        [Header("Transition References")]
        public float speed;
        [SerializeField] private GameObject _transitionGameObject;
        [SerializeField] private Canvas _parentCanvas;
        private RectTransform _transitionRectTransform;
        public RectTransform TransitionRectTransform => _transitionRectTransform;
        public GameObject TransitionGameObject => _transitionGameObject;


        void Awake()
        {
            _transitionGameObject = GameObject.FindGameObjectWithTag("Transition");
            _transitionRectTransform = _transitionGameObject.GetComponent<RectTransform>();
        }

        void Start()
        {
            _parentCanvas = this.gameObject.GetComponentInParent<Canvas>();
            float canvasWidth = _parentCanvas.GetComponent<RectTransform>().rect.width;
            speed = CalculateNormalizedSpeed(canvasWidth, duration);
        }

        public override void InitializeStateMachine()
        {
            // Initialize States
            MenuTransitionStartState startState = new(this);
            MenuTransitionLoadingState loadingState = new(this);
            MenuTransitionEndState endState = new(this);

            _transitions = new List<StateTransition>
            {
                new() {
                    Condition = () => loading,
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

        public void MoveTowards(Vector2 target, float speed)
        {
            _transitionRectTransform.anchoredPosition =
                Vector2.MoveTowards(
                    _transitionRectTransform.anchoredPosition,
                    target,
                    speed * Time.unscaledDeltaTime
                );
        }

        private float CalculateNormalizedSpeed(float canvasWidth, float duration)
        {
            return ((canvasWidth*2) / duration);
        }

        //#######################################

        public void PlayFadeIn() {
            StopAllCoroutines();
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            this.TransitionGameObject.SetActive(true);

            Vector2 target = new Vector2(this.xf, 0);

            while (!Mathf.Approximately(this.TransitionRectTransform.anchoredPosition.x, this.xf))
            {
                this.MoveTowards(target, this.speed);
                yield return null;
            }
           ;

            this.loading = true;
        }

        public void PlayloadingScreen() {
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

        public void PlayFadeOut() {
            StopAllCoroutines();
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            Vector2 target = new Vector2(this.xi, 0);
            yield return new WaitForSeconds(0.25f);

            while (!Mathf.Approximately(this.TransitionRectTransform.anchoredPosition.x, this.xi))
            {
                this.MoveTowards(target, this.speed);
                yield return null;
            }

            Destroy(this.gameObject);
        }
    }
}