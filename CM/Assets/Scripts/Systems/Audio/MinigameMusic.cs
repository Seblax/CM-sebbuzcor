using Gamemanager;
using UnityEngine;

namespace Minigame
{

    public class MinigameMusic : MonoBehaviour
    {
        public AudioSource _audio;
        public float volume = 0.25f;
        public MinigameManager manager => MinigameManager.instance;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this._audio = GetComponent<AudioSource>();
            this._audio.volume = volume;
        }

        void LateUpdate()
        {
            if (manager == null) return;
            if (_audio == null) return;

            // Sincronizar pitch con la aceleración del juego
            this._audio.pitch = Aceleration.Scale;

            // SEGURIDAD: Verificamos que _state no sea null antes de hacer GetType()
            bool isPlaying = manager._state != null &&
                             manager._state is MinigamePlayingState;

            if (isPlaying && this._audio.volume > 0)
            {
                this._audio.volume -= Time.deltaTime / 10;
            }
            else if (!isPlaying && this._audio.volume < volume)
            {
                this._audio.volume += Time.deltaTime / 10;
            }
        }
    }
}