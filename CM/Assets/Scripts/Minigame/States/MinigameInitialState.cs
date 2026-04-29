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
            
            MinigameUIManager.instance.tittle.SetActive(true);
            MinigameUIManager.instance.minigame.SetActive(false);
            MinigameUIManager.instance.score.SetActive(false);

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
