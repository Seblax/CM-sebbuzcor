using StateManagement;
using UnityEngine;

namespace Minigame
{
    public class MinigamePlayingState : IState
    {
        private Minigame minigame;
        float timer;
        bool _moveRequested =  false;

        public MinigamePlayingState(Minigame minigame)
        {
            this.minigame = minigame;
        }

        public void OnEnter()
        {
            if (!GameManager.instance.IsStillAlive) return;

            MinigameUIManager.instance.score.SetActive(true);
            MinigameUIManager.instance.minigame.SetActive(true);

            MinigameUIManager.instance.OnUserChanged.Invoke(MinigameUIManager.instance.GetUser(this.minigame.ID));

            MinigameManager.instance.isPlaying = true;
            timer = 0.75f;
            _moveRequested = false;

            MinigameManager.instance.UpdatePauseState(false);
        }

        public void OnExecute()
        {
            if (_moveRequested) return;

            this.minigame.Timer();
            MinigameUIManager.instance.UpdateHealthBarUI(this.minigame.TimerPercent);

            if (this.minigame.IsTimerOver) {

                timer -= Time.deltaTime;

                if (timer < 0) {
                    _moveRequested = true;

                    MinigameManager.instance.isPlaying = false;
                    MinigameManager.instance.Move?.Invoke();
                }
            }

        }

        public void OnExit()
        {
            MinigameManager.instance.Destroy(MinigameUIManager.instance.minigame);
            MinigameManager.instance.UpdatePauseState(true);

            InputManager.RemoveGameObject();
        }
    }
}
