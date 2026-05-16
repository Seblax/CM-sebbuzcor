using Gamemanager;
using UnityEngine;

namespace Minigame
{
    public class MinigameEndingState : MinigameScoreState
    {
        public MinigameEndingState(Minigame minigame) : base(minigame)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();

            manager.UpdatePauseState(true);

            Lives -= 1;
        }


        public override void OnExecute()
        {
            base.OnExecute();
            if (timer < 1.5f && !moveRequested)
            {
                moveRequested = true;
                AudioManager.instance.PlayEffect("gg");
                Aceleration.Scale = 0.15f;
                manager.StartCoroutine(manager.EndGame());

                ui.UpdateScoreUI(true);
            }
        }

        public override void OnExit()
        {
            GameManager.instance.isGameOver = true;
        }
    }
}
