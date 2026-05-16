using Gamemanager;
using System;

namespace Minigame.Game1
{
    public class BlockBehaviour : PlayerControllerTap
    {
        public Action<float> OnDamageTaken;
        public float health = Data.Minigame.Game1.HEALTH;

        private void Start()
        {
            MinigameManager.instance.minigame.Defeat();
        }

        public override void TapEvent()
        {
            if (IsPaused) return;

            this.health -= UnityEngine.Random.Range(Data.Minigame.Game1.MIN_DMG, Data.Minigame.Game1.MAX_DMG);

            this.health = MathF.Max(this.health, -1);

            OnDamageTaken?.Invoke(this.health);

            if (this.health < 0)
            {
                SetPaused(true);
                MinigameManager.instance.minigame.MinigameScoreValue = (int)((MinigameManager.instance.minigame.minigameTimer * Data.Minigame.Game1.RATIO_SCORE_POINTS_TIMER + Data.Minigame.Game1.BASE_SCORE_POINTS));

                MinigameManager.instance.minigame.Victory();
                
                AudioManager.instance.PlayEffect(Data.Minigame.Game1.WIN_SOUND);
            }
        }
    }
}