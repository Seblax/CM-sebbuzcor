using ShakeAnimation;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame
{
    public class UIBrain : MonoBehaviour
    {
        public Image image;

        [System.Serializable]
        public class BrainLevel
        {
            public float threshold;
            public BrainScriptableObject data;
            [HideInInspector] public bool triggered;
        }

        [Header("Brain Levels")]
        public BrainLevel[] levels;

        private Shake _shake;

        private void Start()
        {
            image = GetComponent<Image>();
            _shake = GetComponent<Shake>();

            MinigameUIManager.instance.OnHealthBarChanged += HandleBrainLevels;

            foreach (var item in levels)
            {
                item.triggered = false;
            }

            Apply(levels[0].data);
        }

        private void HandleBrainLevels(float timerPercent)
        {
            foreach (var level in levels)
            {
                if (level.triggered) continue;

                if (timerPercent <= level.threshold)
                {
                    Apply(level.data);
                    level.triggered = true;
                }
            }

            if (AllLevelsTriggered())
            {
                MinigameUIManager.instance.OnHealthBarChanged -= HandleBrainLevels;
            }
        }

        private bool AllLevelsTriggered()
        {
            foreach (var level in levels)
            {
                if (!level.triggered) return false;
            }
            return true;
        }

        private void Apply(BrainScriptableObject brain)
        {
            image.sprite = brain.brainSprite;

            if (brain.soundEffect != null)
                AudioManager.instance.PlayEffect(brain.soundEffect);

            _shake.Play(brain.speed, brain.interval, brain.duration);
        }
    }
}