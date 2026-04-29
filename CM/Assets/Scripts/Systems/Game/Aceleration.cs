
using UnityEngine;

namespace Gamemanager
{
    public class Aceleration : MonoBehaviour
    {
        private static float SCALE = 1;
        public static float GetScale { get => SCALE; }
        public static float SetScale
        {
            set
            {
                SCALE = value;
                Time.timeScale = SCALE;
                SetTimeScale();
            }
        }

        public static float ResetScale
        {
            set
            {
                SCALE = 1;
                Time.timeScale = SCALE;
            }
        }

        static void SetTimeScale()
        {
            Time.timeScale = SCALE;
        }
    }
}