using Gamemanager;
using UnityEngine;

namespace Minigame
{

    public class MinigameMusic : MonoBehaviour
    {
        public AudioSource audio;
        public float volume = 0.25f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this.audio = GetComponent<AudioSource>();
            this.audio.volume = volume;
        }

        // Update is called once per frame
        void Update()
        {
            this.audio.pitch = Aceleration.Scale;

            if (MinigameManager.instance._state.GetType().Equals(typeof(MinigamePlayingState)) && this.audio.volume > 0)
            {
                this.audio.volume -= Time.deltaTime / 10;
            }
            else if (this.audio.volume < volume)
            {
                this.audio.volume += Time.deltaTime / 10;
            }
        }
    }
}