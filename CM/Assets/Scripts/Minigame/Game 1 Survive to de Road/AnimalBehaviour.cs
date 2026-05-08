using UnityEngine;
using Minigame;
using Gamemanager;
namespace Game0
{

    public class AnimalBehaviour : PlayerControllerTap, IPausable
    {

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite[] bunny;

        public Hop _hop;
        public bool dead = false;


        [SerializeField] bool paused = true;
        public bool IsPaused { get => paused; }

        void Start()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            dead = false;
        }

        public override void OnEnable()
        {
            base.OnEnable();
            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause += SetPaused;
        }

        void Update()
        {
            if (IsPaused) return;

            Sprite sprite = this._hop.IsHopping ? bunny[1] : bunny[0];
            sprite = this.dead ? bunny[2] : sprite;

            _renderer.sprite = sprite;

            if (dead) return;
            
            MinigameManager.instance.minigame.Victory();
            
            if (MinigameManager.instance.minigame.Win && MinigameManager.instance.minigame.IsTimerOver)
            {
                GameManager.instance.score += 100 * (int)Aceleration.Scale;
            }
        }

        public override void TapEvent()
        {
            if (IsPaused) return;

            if (_hop.IsHopping || dead) return;
            _hop.Play();
            AudioManager.instance.PlayEffect("BunnyJump");
        }

        public void Hit()
        {
            GameManager.instance.score -= (int)+100;

            this.dead = true;
            _hop.Stop();
            MinigameManager.instance.minigame.Defeat();
            AudioManager.instance.PlayEffect("BunnyHit");

            base.OnDisable();
        }

        public void SetPaused(bool isPaused)
        {
            paused = isPaused;
        }
    }

}