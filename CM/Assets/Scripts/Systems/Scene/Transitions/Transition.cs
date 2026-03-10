using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ui
{
    public class Transition : MonoBehaviour
    {
        public string targetScenename;

        private AsyncOperation operation;

        public virtual void PlayTransitionIn()
        {
            LoadTargetScene();
        }

        public virtual void LoadTargetScene()
        {
            if (operation != null) return;
            operation = SceneManager.LoadSceneAsync(targetScenename);
            StartCoroutine(LoadSceneRoutine());
        }

        private IEnumerator LoadSceneRoutine()
        {
            operation.allowSceneActivation = false;

            while (operation.progress < 0.9f)
            {
                yield return null;
            }

            PlayTransitionOut();
        }

        public virtual void PlayTransitionOut()
        {
            operation.allowSceneActivation = true;
        }
    }
}