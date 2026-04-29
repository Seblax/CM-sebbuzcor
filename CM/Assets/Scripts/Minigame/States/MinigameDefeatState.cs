using StateManagement;
using UnityEngine;

namespace Minigame
{
    public class MinigameDefeatState : IState
    {
        private Minigame minigame;

        public MinigameDefeatState(Minigame minigame)
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
