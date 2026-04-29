using StateManagement;
using UnityEngine;

namespace ShakeAnimation
{
    public class ShakeStart : IState
    {
        Shake _shake;
        Transform _transform;
        
        float offsetX;
        float offsetY;

        public ShakeStart(Shake shake)
        {
            this._shake = shake;
            this._transform = shake.transform;
        }

        public void OnEnter()
        {
            this._shake.gameObject.SetActive(true);
            this._shake.isPlaying = true;

            offsetX = Random.Range(0, 100);
            offsetY = Random.Range(0, 100);
        }

        public void OnExecute()
        {
            this._shake.isPlaying = this._shake.Duration > 0;
            this._shake.Duration -= Time.deltaTime;

            float y = Position(offsetY);
            float x = Position(offsetX);
            _transform.localPosition = this._shake.startPosition + Vector3.up * y + Vector3.right * x;
        }

        public void OnExit()
        {
            this._transform.localPosition = this._shake.startPosition;
        }

        float Position(float offset) {
           return Mathf.Cos(Time.time * this._shake.Speed * Mathf.PI + offset) * this._shake.Interval;
        }
    }
}