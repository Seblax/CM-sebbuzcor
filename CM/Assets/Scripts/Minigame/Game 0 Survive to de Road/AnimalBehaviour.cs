using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Minigame.Game0
{
    public class AnimalBehaviour : PlayerControllerTap
    {
        private SpriteRenderer _renderer;
        [SerializeField] private Sprite[] bunny;

        private Hop hop;
        private bool dead = false;

        [SerializeField] private CarBehaviour car;

        void Start()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            car = FindAnyObjectByType(typeof(CarBehaviour)).GetComponent<CarBehaviour>();

            SetHopConfiguration();

            dead = false;
        }

        void Update()
        {
            if (IsPaused) return;
            car.DisableCarCollider(this.transform.localPosition.x);

            Sprite sprite = this.hop.IsHopping ? bunny[1] : bunny[0];
            sprite = this.dead ? bunny[2] : sprite;

            _renderer.sprite = sprite;

            if (dead) return;

            MinigameManager.instance.minigame.Victory();
        }

        public override void TapEvent()
        {
            if (IsPaused) return;

            if (hop.IsHopping || dead) return;
            if (hop != null) hop.Play();
            AudioManager.instance.PlayEffect(Data.Minigame.Game0.Bunny.JUMP_SOUND);
        }

        public void Hit()
        {
            this.dead = true;
            if (hop != null) hop.Stop();
            MinigameManager.instance.minigame.Defeat();
            AudioManager.instance.PlayEffect(Data.Minigame.Game0.Bunny.HIT_SOUND);
        }

        void SetHopConfiguration()
        {
            hop.amplitude = Data.Minigame.Game0.Bunny.Hop.AMPLITUDE;
            hop.speed = Data.Minigame.Game0.Bunny.Hop.SPEED;
            hop.direction = Data.Minigame.Game0.Bunny.Hop.DIRECTION;
        }
    }
}