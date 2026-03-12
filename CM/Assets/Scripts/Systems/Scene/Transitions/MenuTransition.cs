using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ui
{
    public class MenuTransition : Transition
    {
        [Header("Transition Settings")]
        public float xi = -2500;
        public float xf = 2500;
        public float duration = 0.25f;

        [Header("Transition References")]
        public float speed;
        [SerializeField] private GameObject _transitionGameObject;
        [SerializeField] private Canvas _parentCanvas;
        private RectTransform _transitionRectTransform;

        Coroutine _animation;

        void Awake()
        {
            _transitionGameObject = GameObject.FindGameObjectWithTag("Transition");
        }

        void Start()
        {
            _parentCanvas = this.gameObject.GetComponentInParent<Canvas>();
            float canvasWidth = _parentCanvas.GetComponent<RectTransform>().rect.width;
            speed = CalculateNormalizedSpeed(canvasWidth, duration);
        }

        public override void PlayTransitionIn(string sceneName)
        {
            if (_animation != null) return;
            this.sceneName = sceneName;

            _transitionRectTransform = _transitionGameObject.GetComponent<RectTransform>();

            // Posición inicial
            _transitionRectTransform.anchoredPosition = new Vector2(xi, 0);

            _animation = StartCoroutine(FadeIn());
        }

        public override void LoadTargetScene()
        {
            if (operation != null) return;

            operation = SceneManager.LoadSceneAsync(sceneName);
            StartCoroutine(LoadSceneRoutine());
        }

        public override void PlayTransitionOut()
        {
            _animation = StartCoroutine(FadeOut());
        }

        private void MoveTowards(Vector2 target, float speed)
        {
            _transitionRectTransform.anchoredPosition =
                Vector2.MoveTowards(
                    _transitionRectTransform.anchoredPosition,
                    target,
                    speed * Time.deltaTime
                );
        }

        private float CalculateNormalizedSpeed(float canvasWidth, float duration)
        {
            return ((canvasWidth*2) / duration);
        }

        private IEnumerator FadeIn()
        {
            _transitionGameObject.SetActive(true);

            Vector2 target = new Vector2(xf, 0);

            while (!Mathf.Approximately(_transitionRectTransform.anchoredPosition.x, xf))
            {
                MoveTowards(target, speed);
                yield return null;
            }

            base.PlayTransitionIn(this.sceneName);
        }

        private IEnumerator LoadSceneRoutine()
        {
            operation.allowSceneActivation = false;

            while (operation.progress < 0.9f)
            {
                yield return null;
            }

            operation.allowSceneActivation = true;

            PlayTransitionOut();
        }

        private IEnumerator FadeOut()
        {
            Vector2 target = new Vector2(xi, 0);
            yield return new WaitForSeconds(0.25f);

            while (!Mathf.Approximately(_transitionRectTransform.anchoredPosition.x, xi))
            {
                MoveTowards(target, speed);
                yield return null;
            }

            base.PlayTransitionOut();
            _transitionGameObject.SetActive(false);
        }
    }
}