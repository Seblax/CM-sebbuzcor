using StateManagement;
using UnityEngine;

namespace Minigame
{
    public class MinigameDefeatState : IState
    {
        private Minigame minigame;
        private float timer;
        private int initialLives;

        public MinigameDefeatState(Minigame minigame)
        {
            this.minigame = minigame;
        }

        public void OnEnter()
        {
            initialLives = GameManager.instance.GetLives;
            GameManager.instance.SetLives -=1;
            AudioManager.instance.PlayEffect("LoseGame");


            MinigameUIManager.instance.OnLivesChanged.Invoke(initialLives, false);
            timer = 3f;
        }

        public void OnExecute()
        {
            Debug.Log($"Defeat State Executing. Lifes: {GameManager.instance.GetLives}");
            timer -= Time.deltaTime;
            if (timer < 1.5f)
            {
                MinigameUIManager.instance.OnLivesChanged.Invoke(GameManager.instance.GetLives, true);
            }

            MinigameManager.instance.isScoreScreenOver = timer < 0;
        }

        public void OnExit()
        {
            if (!GameManager.instance.IsStillAlive) return;

            MinigameUIManager.Remove();
            GameManager.instance.DestroyGameObject(MinigameUIManager.instance.gameObject, 2.5f);

            MinigameManager.instance.Move?.Invoke();
            MinigameManager.RemoveGameObject();

            MinigameManager.Create();
        }
    }
}
