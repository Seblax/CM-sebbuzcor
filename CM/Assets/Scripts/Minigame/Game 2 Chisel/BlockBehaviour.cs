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

        public override void TapEvent()
        {
            if (IsPaused) return;

            this.health -= UnityEngine.Random.Range(5, 10);

            this.health = MathF.Max(this.health, -1);

            OnDamageTaken?.Invoke(this.health);

            if (this.health < 0)
            {
                SetPaused(true);
                MinigameManager.instance.minigame.MinigameScoreValue += MinigameManager.instance.minigame.minigameTimer * 100;
                MinigameManager.instance.minigame.Victory();
                AudioManager.instance.PlayEffect("BlockWin");
            }
        }
    }
}