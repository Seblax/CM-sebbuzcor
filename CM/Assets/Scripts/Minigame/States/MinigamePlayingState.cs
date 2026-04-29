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
            MinigameUIManager.instance.score.SetActive(true);
            MinigameUIManager.instance.minigame.SetActive(true);

            MinigameManager.instance.isPlaying = true;
            timer = 2.5f;
            _moveRequested = false;
        }

        public void OnExecute()
        {
            if (_moveRequested) return;

            this.minigame.Timer();
            MinigameUIManager.instance.OnHealthBarChanged(this.minigame.TimerPercent);

            if (this.minigame.IsTimerOver) {
                //Ejecutar victoria o derrota
                var placehodlder = Random.Range(0, 1f);
                var message = placehodlder >= 0.5 ? "Perdiste!" : "Ganaste!";

                Debug.Log(message);

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
        }
    }
}
