using StateManagement;
using UnityEngine;

namespace Minigame
{
    public class MinigameVictoryState : IState
    {
        private Minigame minigame;
        private float timer;

        public MinigameVictoryState(Minigame minigame)
        {
            this.minigame = minigame;
        }

        public void OnEnter()
        {
         
            AudioManager.instance.PlayEffect("WinGame");
            MinigameUIManager.instance.OnLivesChanged.Invoke(GameManager.instance.GetLives, false);
            timer = 3f;
        }

        public void OnExecute()
        {
            timer -= Time.deltaTime;
            MinigameManager.instance.isScoreScreenOver = timer < 0;
        }

        public void OnExit()
        {
            MinigameUIManager.Remove();
            GameManager.instance.DestroyGameObject(MinigameUIManager.instance.gameObject, 2.5f);

            MinigameManager.instance.Move?.Invoke();
            MinigameManager.RemoveGameObject();

            MinigameManager.Create();
        }
    }
}
