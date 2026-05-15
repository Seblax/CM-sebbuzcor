using StateManagement;
using UnityEngine;

namespace Minigame
{
    public class MinigameTittleState : MinigameState
    {
        private float _timer;

        public MinigameTittleState(Minigame minigame) : base(minigame)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            mover.OnStartMove(() => SetActiveOn(ui.minigame));  // Show the minigame container when the move starts
            mover.OnStartMove(() => ui.UpdateMinigameUI(ui.GetUser(minigame.ID)));  // Update the user info when the move starts

            manager.UpdatePauseState(true);                     // Pause the minigame while the title is displayed

            _timer = 5f;
        }

        public override void OnExecute()
        {
            if (!GameManager.instance.IsStillAlive) return;

            if (moveRequested) return;

            _timer -= Time.deltaTime;

            manager.isTittleOver = _timer < 0;

            if (MinigameManager.instance.isTittleOver)
            {
                moveRequested = true;
                mover.Play();
            }
        }

        public override void OnExit()
        {
            manager.DestroyGameObject(ui.tittle);
        }
    }
}
