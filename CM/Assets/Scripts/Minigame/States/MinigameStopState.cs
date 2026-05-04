using Gamemanager;
using StateManagement;
using ui;
using UnityEngine;

namespace Minigame
{
    public class MinigameStopState : IState
    {
        private float timer;
        private int initialLives;

        public void OnEnter()
        {
            MinigameManager.instance.Pause?.Invoke(true);

            AudioManager.instance.PlayEffect("gg");

            Aceleration.Scale = 0.15f;

            MinigameManager.instance.StartCoroutine(MinigameManager.instance.EndGame());
        }

        public void OnExecute()
        {
        
        }

        public void OnExit()
        {
            if (!GameManager.instance.IsStillAlive) return;
        }
    }
}
