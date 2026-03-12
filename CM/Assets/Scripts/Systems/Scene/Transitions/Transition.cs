using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ui
{
    public class Transition : MonoBehaviour
    {
        public string sceneName = string.Empty;
        public AsyncOperation operation;

        public virtual void PlayTransitionIn(string sceneName)
        {
            this.sceneName = sceneName;
            LoadTargetScene();
        }

        public virtual void LoadTargetScene()
        {
            if (operation != null) return;
            operation = SceneManager.LoadSceneAsync(sceneName);
            StartCoroutine(LoadSceneRoutine());
        }


        public virtual void PlayTransitionOut()
        {
            Destroy(this.gameObject);
        }

        private IEnumerator LoadSceneRoutine()
        {
            operation.allowSceneActivation = false;
            yield return new WaitForSeconds(0.5f);

            while (operation.progress < 0.9f)
            {
                yield return null;
            }

            operation.allowSceneActivation = true;
            PlayTransitionOut();
        }
    }
}