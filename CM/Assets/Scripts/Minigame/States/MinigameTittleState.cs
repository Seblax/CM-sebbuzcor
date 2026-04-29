using StateManagement;
using UnityEngine;

namespace Minigame
{
    public class MinigameTittleState : IState
    {
        private Minigame minigame;
        MinigameUIManager UI;

        float timer;
        bool _moveRequested = false;

        public MinigameTittleState(Minigame minigame)
        {
            this.minigame = minigame;
        }

        public void OnEnter()
        {
            timer = 5f;
            _moveRequested = false;
            UI = MinigameUIManager.instance;
            UI.UpdateTittleUI(UI.GetTittle(minigame.ID));
        }

        public void OnExecute()
        {
            if (_moveRequested) return;

            timer -= Time.deltaTime;

            MinigameManager.instance.isTittleOver = timer < 0;

            if (MinigameManager.instance.isTittleOver)
            {
                _moveRequested = true;
                MinigameManager.instance.Move?.Invoke();
            }
        }

        public void OnExit()
        {

        }
    }
}
