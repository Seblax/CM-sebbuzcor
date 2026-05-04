using Gamemanager;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Minigame.Game2
{
    public class BlockBehaviour : PlayerControllerTap, IPausable
    {
        public Action<float> OnDamageTaken;
        public float health = 100f;

        [SerializeField] bool paused = true;
        public bool IsPaused { get => paused; }


        public override void OnEnable()
        {
            base.OnEnable();
            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause += SetPaused;
        }

        public override void TapEvent()
        {
            if (IsPaused) return;
            MinigameManager.instance.minigame.Defeat();

            this.health -= UnityEngine.Random.Range(5, 10);

            this.health = MathF.Max(this.health, -1);

            OnDamageTaken?.Invoke(this.health);

            if (this.health <= 0)
            {
                this.paused = true;
                GameManager.instance.score += (int)((MinigameManager.instance.minigame.minigameTimer * 100 + 100)*Aceleration.Scale);
                MinigameManager.instance.minigame.Victory();
                AudioManager.instance.PlayEffect("BlockWin");
            }

        }

        public void SetPaused(bool isPaused)
        {
            this.paused = isPaused;
        }
    }

}