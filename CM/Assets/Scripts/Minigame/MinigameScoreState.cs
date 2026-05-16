using UnityEngine;

namespace Minigame
{
    public class MinigameScoreState : MinigameState
    {
        public float timer = 3;
        public int Lives
        {
            get => GameManager.instance.GetLives;
            set => GameManager.instance.SetLives = value;
        }

        public MinigameScoreState(Minigame minigame) : base(minigame)
        {
            timer = 3;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            ui.UpdateScoreUI(false);
        }

        public override void OnExecute()
        {
            
            timer -= Time.deltaTime;
            manager.isScoreScreenOver = timer < 0;

            if(manager.isScoreScreenOver && !moveRequested)
            {
                mover.Play();
                moveRequested = true;
            }
        }

        public override void OnExit()
        {
            if (!GameManager.instance.IsStillAlive) return;

            MinigameUIManager.Remove();
            GameManager.instance.DestroyGameObject(ui.gameObject, 0.5f);

            MinigameManager.RemoveGameObject();
            MinigameManager.Create();
        }
    }
}