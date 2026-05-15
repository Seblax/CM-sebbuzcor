using StateManagement;
using UnityEngine;

namespace Minigame
{
    public class MinigameState : IState
    {
        public MinigameUIManager ui;
        public MinigameManager manager;
        public Minigame minigame;
        public MinigameMover mover;

        public bool moveRequested = false;

        public MinigameState(Minigame minigame)
        {
            this.ui = MinigameUIManager.instance;
            this.manager = MinigameManager.instance;
            this.minigame = minigame;
            this.mover = manager.Mover;
            this.moveRequested = false;
        }

        public virtual void OnEnter()
        {
            moveRequested = false;
        }

        public virtual void OnExecute()
        {

        }

        public virtual void OnExit()
        {

        }

        public void SetActiveOn(GameObject @object)
        {
            if (@object != null)
                @object.SetActive(true);
        }

        public void SetActiveOff(GameObject @object)
        {
            if (@object != null)
                @object.SetActive(false);
        }
    }
}