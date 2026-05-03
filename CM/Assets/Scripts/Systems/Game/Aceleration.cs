using UnityEngine;

namespace Gamemanager
{
    public class Aceleration
    {
        private static float _scale = 1f;

        public static float Scale
        {
            get => Mathf.Min(3f, _scale);
            set
            {
                _scale = value;
                Time.timeScale = Mathf.Min(3f, _scale);

                // Ahora el Debug se ejecutará sin problemas
                Debug.LogWarning($"Incremented Time Scale set to: {Mathf.Min(3f, _scale)}");
            }
        }

        // Método para resetear
        public static void Reset()
        {
            Scale = 1f; // Esto activará el set y el Debug
        }

        public static void IncrementTimeScale()
        {
            // Accedemos a la propiedad Scale
            Scale *= 1.25f;
        }
    }
}