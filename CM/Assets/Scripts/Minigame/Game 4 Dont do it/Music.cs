using UnityEngine;

namespace Minigame.Game4
{
    public class Music : MonoBehaviour, IPausable
    {
        [SerializeField] private Sound musicSound;
        [SerializeField] private Sound quietSound;

        [SerializeField] private PlayerInputDetector detector;

        private bool paused = true;
        public bool IsPaused => paused;


        void Start()
        {
            detector = GetComponent<PlayerInputDetector>();
            detector.PlayerInputDetected += StopMusic;
        }

        private void OnEnable()
        {
            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause += SetPaused;
        }

        private void OnDisable()
        {
            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause -= SetPaused;
        }

        void Update()
        {
            if (IsPaused) return;

            if (musicSound == null)
            {
                quietSound = AudioManager.instance.PlayEffect(Data.Minigame.Game4.QUIET_SOUND);
                musicSound = AudioManager.instance.PlayEffect(Data.Minigame.Game4.MUSIC_SOUND);
                SetPaused(true);
            }
        }

        public void SetPaused(bool isPaused)
        {
            this.paused = isPaused;
        }

        void StopMusic()
        {
            if (musicSound != null)
            {
                Destroy(musicSound.gameObject);
                Destroy(quietSound.gameObject);
            }
        }
    }
}