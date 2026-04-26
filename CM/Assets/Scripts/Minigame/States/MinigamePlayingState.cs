using StateManagement;
using UnityEngine;

namespace Minigame
{
    public class MinigamePlayingState : IState
    {
        private Minigame minigame;

        public MinigamePlayingState(Minigame minigame)
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
