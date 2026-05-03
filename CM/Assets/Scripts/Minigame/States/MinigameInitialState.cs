using StateManagement;
using System.Threading;
using UnityEngine;

namespace Minigame
{
    // Solo si es el primer juego
    public class MinigameInitialState : IState
    {
        bool _moveRequested = false;

        public void OnEnter()
        {

        }

        public void OnExecute()
        {
            if (_moveRequested) return;

            if (!MinigameManager.instance.isMoving)
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
