using Gamemanager;
using UnityEngine;

namespace Minigame
{

    public class MinigameMusic : MonoBehaviour
    {
        public AudioSource _audio;
        public float volume = 0.25f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this._audio = GetComponent<AudioSource>();
            this._audio.volume = volume;
        }

        // Update is called once per frame
        void Update()
        {
            this._audio.pitch = Aceleration.Scale;

            if (MinigameManager.instance._state.GetType().Equals(typeof(MinigamePlayingState)) && this._audio.volume > 0)
            {
                this._audio.volume -= Time.deltaTime / 10;
            }
            else if (this._audio.volume < volume)
            {
                this._audio.volume += Time.deltaTime / 10;
            }
        }
    }
}