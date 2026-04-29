using StateManagement;
using System.Threading;
using UnityEngine;

namespace Minigame
{
    // Solo si es el primer juego
    public class MinigameInitialState : IState
    {
        float timer = 0;

        public void OnEnter()
        {
            MinigameManager.instance.isStarting = true;
            timer = 2.5f;
        }

        public void OnExecute()
        {
            timer -= Time.deltaTime;

            if (timer < 0) {
                MinigameManager.instance.isStarting = false;
            }
        }

        public void OnExit()
        {

        }
    }
}
