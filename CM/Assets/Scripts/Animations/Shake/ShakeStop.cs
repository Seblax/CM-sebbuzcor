using StateManagement;
using UnityEngine;

namespace ShakeAnimation
{
    public class ShakeStop : IState
    {
        Shake _shake;

        public ShakeStop(Shake shake)
        {
            this._shake = shake;
        }

        public void OnEnter()
        {
            this._shake.gameObject.SetActive(true);
            this._shake.isPlaying = false;
            this._shake.play = false;
        }

        public void OnExecute()
        {
        }

        public void OnExit()
        {
        }
    }
}
