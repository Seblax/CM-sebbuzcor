using UnityEngine;
using Minigame;
using Gamemanager;
namespace Game0
{

    public class AnimalBehaviour : PlayerControllerTap
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite[] bunny;

        public Hop _hop;
        public bool dead = false;


        void Start()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            dead = false;

            MinigameManager.instance.minigame.MinigameScoreValue = 250f;
        }

        void Update()
        {
            if (IsPaused) return;

            Sprite sprite = this._hop.IsHopping ? bunny[1] : bunny[0];
            sprite = this.dead ? bunny[2] : sprite;

            _renderer.sprite = sprite;

            if (dead) return;

            MinigameManager.instance.minigame.Victory();
        }

        public override void TapEvent()
        {
            if (IsPaused) return;

            if (_hop.IsHopping || dead) return;
            if (_hop != null) _hop.Play();
            AudioManager.instance.PlayEffect("BunnyJump");
        }

        public void Hit()
        {
            this.dead = true;
            if (_hop != null) _hop.Stop();
            MinigameManager.instance.minigame.Defeat();
            AudioManager.instance.PlayEffect("BunnyHit");
        }
    }
}