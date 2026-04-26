using StateManagement;
using UnityEngine;

namespace Minigame
{
    public class MinigameVictoryState : IState
    {
        private Minigame minigame;

        public MinigameVictoryState(Minigame minigame)
        {
            this.minigame = minigame;
        }

        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExecute()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}
