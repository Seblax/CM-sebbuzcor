using Gamemanager;
using System;
using UnityEngine;

namespace Minigame.Game2
{
    public class BlockBehaviour : PlayerControllerTap
    {
        public Action<float> OnDamageTaken;
        public float health = 100f;

        private void Start()
        {
            MinigameManager.instance.minigame.Defeat();
        }
        public override void OnEnable()
        {
            base.OnEnable();

            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause += SetPaused;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause -= SetPaused;
        }


        public override void TapEvent()
        {
            if (IsPaused) return;

            this.health -= UnityEngine.Random.Range(5, 10);

            this.health = MathF.Max(this.health, -1);

            OnDamageTaken?.Invoke(this.health);

            if (this.health < 0)
            {
                SetPaused(true);
                GameManager.instance.score += (int)((MinigameManager.instance.minigame.minigameTimer * 100 + 100) * Aceleration.Scale);
                MinigameManager.instance.minigame.Victory();
                AudioManager.instance.PlayEffect("BlockWin");
            }
        }
    }
}