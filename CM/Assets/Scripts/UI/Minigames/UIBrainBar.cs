using UnityEngine;
using UnityEngine.UI;

namespace Minigame
{
    public class UIBrainBar : MonoBehaviour
    {
        public Image image;

        [System.Serializable]
        public class BarLevel
        {
            public float threshold;
            public MinigameScriptableObject data;
        }

        [Header("Bar Levels")]
        public BarLevel[] levels;

        private void Start()
        {
            image = GetComponent<Image>();

            MinigameManager.instance.UpdateUI += HandleBarLevels;
            MinigameManager.instance.UpdateUI += HandleBarLenght;
        }

        private void HandleBarLenght(float timerPercent)
        {
            this.image.fillAmount = timerPercent;
        }

        private void HandleBarLevels(float timerPercent)
        {
            foreach (var level in levels)
            {
                if (timerPercent <= level.threshold)
                {
                    Apply(level.data);
                }
            }
        }

        private void Apply(MinigameScriptableObject bar)
        {
            image.sprite = bar.barSprite;
        }
    }

}