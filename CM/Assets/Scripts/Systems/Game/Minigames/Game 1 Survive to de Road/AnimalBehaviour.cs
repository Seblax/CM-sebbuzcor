using UnityEngine;
using Minigame;
namespace Game0
{

    public class AnimalBehaviour : PlayerControllerTap, IPausable
    {

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite[] bunny;

        public Hop _hop;
        public bool dead = false;


        bool paused = true;
        public bool IsPaused { get => paused; }

        void Start()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            MinigameManager.instance.minigame.Victory();
            dead = false;
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
            if(IsPaused) return;

            Sprite sprite = this._hop.IsHopping ? bunny[1] : bunny[0];
            sprite = this.dead ? bunny[2] : sprite;

            _renderer.sprite = sprite;
        }

        public override void TapEvent()
        {
            if (IsPaused) return;

            if (_hop.IsHopping || dead) return;
            _hop.Play();
        }

        public void Hit() { 
            this.dead = true;
            _hop.Stop();
            MinigameManager.instance.minigame.Defeat();

            ///////////////// Sonido de muerte
        }

        public void SetPaused(bool isPaused)
        {
            paused = isPaused;
        }
    }

}