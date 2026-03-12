using UnityEngine;

namespace ui
{
    /// <summary>
    /// Esta clase sirve únicamente como puente para UnityEvents en el Inspector
    /// para poder llamar al TransitionManager sin referenciarlo directamente.
    /// </summary>
    public class TransitionInvoker : MonoBehaviour
    {
        /// <summary>
        /// Llama al TransitionManager para hacer la transición a otra escena.
        /// </summary>
        public void TransitionTo(string sceneName)
        {
            if (TransitionManager.instance != null)
            {
                TransitionManager.instance.TransitionTo(sceneName);
            }
            else
            {
                Debug.LogWarning("TransitionManager no existe en la escena.");
            }
        }

        /// <summary>
        /// Permite asignar el prefab de transición desde UnityEvents si es necesario.
        /// </summary>
        public void SetTransitionPrefab(GameObject prefab)
        {
            if (TransitionManager.instance != null)
            {
                TransitionManager.instance.TransitionPrefab = prefab;
            }
            else
            {
                Debug.LogWarning("TransitionManager no existe en la escena.");
            }
        }
    }
}