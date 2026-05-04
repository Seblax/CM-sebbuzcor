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

            TransitionManager.instance.SetTransitionPrefab(Resources.Load<GameObject>("Prefabs/UI/Transitions/TransitionMenu"));
            TransitionManager.instance.TransitionTo("PrincipalMenu");
            timer = 3f;

            ///Destruirlo Todo
        }

        public void OnExecute()
        {
            timer -= Time.unscaledDeltaTime;

            Debug.Log($"Stop State Executing. Timer: {timer}");

            if (timer <= 0)
            {
            }
        }

        public void OnExit()
        {

        }
    }
}
