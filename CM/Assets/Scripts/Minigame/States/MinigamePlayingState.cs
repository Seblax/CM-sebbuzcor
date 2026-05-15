using StateManagement;
using UnityEngine;

namespace Minigame
{
    public class MinigamePlayingState : MinigameState
    {
        float timer;

        public MinigamePlayingState(Minigame minigame) : base(minigame)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (!GameManager.instance.IsStillAlive) return;

            mover.OnStartMove(() => SetActiveOn(ui.score));
            mover.OnStartMove(() => ui.UpdateScoreUI(false));

            manager.isPlaying = true;
            timer = 0.75f;

            manager.UpdatePauseState(false);
        }

        public override void OnExecute()
        {
            if (moveRequested) return;

            minigame.Timer();
            ui.UpdateHealthBarUI(minigame.TimerPercent);

            if (minigame.IsTimerOver) {

                timer -= Time.deltaTime;

                if (timer < 0) {
                    moveRequested = true;
                    manager.isPlaying = false;
                    mover.Play();
                }
            }

        }

        public override void OnExit()
        {
            manager.UpdatePauseState(true);
            manager.DestroyGameObject(ui.minigame);
        }
    }
}
