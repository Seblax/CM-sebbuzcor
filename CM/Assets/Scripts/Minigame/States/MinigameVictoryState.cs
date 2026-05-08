using StateManagement;
using UnityEngine;

namespace Minigame
{
    public class MinigameVictoryState : IState
    {
        private float timer;

        public void OnEnter()
        {
            AudioManager.instance.PlayEffect("WinGame");
            MinigameUIManager.instance.OnLivesChanged.Invoke(GameManager.instance.GetLives, false);

            GameManager.instance.Score = (int) MinigameManager.instance.minigame.MinigameScoreValue;

            timer = 3f;
        }

        public void OnExecute()
        {
            timer -= Time.deltaTime;
            MinigameManager.instance.isScoreScreenOver = timer < 0;
        }

        public void OnExit()
        {
            if(!GameManager.instance.IsStillAlive) return;

            MinigameUIManager.Remove();
            GameManager.instance.DestroyGameObject(MinigameUIManager.instance.gameObject, 2.5f);

            MinigameManager.instance.Move?.Invoke();
            MinigameManager.RemoveGameObject();

            MinigameManager.Create();
        }
    }
}
