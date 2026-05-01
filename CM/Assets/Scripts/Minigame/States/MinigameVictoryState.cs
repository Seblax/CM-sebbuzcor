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
            Debug.Log("Ganaste!");
        }

        public void OnExecute()
        {

        }

        public void OnExit()
        {

        }
    }
}
