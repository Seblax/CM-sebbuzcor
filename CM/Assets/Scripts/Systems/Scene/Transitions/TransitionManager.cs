using System.IO;
using UnityEngine;

namespace ui
{
    public class TransitionManager : Singleton<TransitionManager>
    {
        private GameObject transitionPrefab;
        private Canvas _canvas;

        public GameObject TransitionPrefab { set => transitionPrefab = value; }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return; // importante salir para no ejecutar el resto
            }

            DontDestroyOnLoad(gameObject);

            // Configurar Canvas si no existe
            _canvas = GetComponent<Canvas>();
            if (_canvas == null)
            {
                _canvas = gameObject.AddComponent<Canvas>();
                _canvas.sortingLayerName = "UI";
                _canvas.sortingOrder = 50;
            }
        }
        public void SetTransitionPrefab(GameObject prefab) 
        { transitionPrefab = prefab; }


        public void TransitionTo(string scene) {
            if (transitionPrefab == null) throw new IOException("Transition Object is null") ;
            
            GameObject transitionObject = Instantiate(transitionPrefab, transform);
            Transition transition = transitionObject.GetComponent<Transition>();

            if (transition == null) throw new IOException("Transition Object doesn't have Transition component.");

            SetTransitionManagerAsParent(transitionObject);
            transition.PlayTransitionIn(scene);
        }

        private void LateUpdate()
        {
            _canvas.worldCamera = Camera.main;
        }

        private void SetTransitionManagerAsParent(GameObject transitionObject) {
            RectTransform rect = transitionObject.GetComponent<RectTransform>();
            rect.localPosition = Vector3.zero;
            rect.localScale = Vector3.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }
    }
}
