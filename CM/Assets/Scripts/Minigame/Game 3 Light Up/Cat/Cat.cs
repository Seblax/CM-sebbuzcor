using Animation;
using System;
using UnityEngine;



namespace Minigame.Game3
{

    public class Cat : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] sprites;

        Shake _shake;
        Hop _hop;

        [SerializeField] private float timer;
        [SerializeField] private Vector3 currentPosition;
        public Action<float> UpdateTombSpriteLayer;

        public bool Shaking => _shake.isPlaying;
        public bool IsCatched => timer <= 0;

        void Awake()
        {
            this.sprites = Resources.LoadAll<Sprite>(Data.Minigame.Game3.Cat.CAT_SPRITES_PATH);

            this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            this._shake = GetComponentInChildren<Shake>();
            this._hop = GetComponentInChildren<Hop>();

            this.spriteRenderer.sprite = sprites[0];

            float scaleRatio = Data.Minigame.Game3.Cat.SCALE_RATIO;

            this.transform.localScale *= UnityEngine.Random.Range(1 - scaleRatio, 1 + scaleRatio);
        }

        public void SetPosition()
        {
            Vector3 LanternPosition = FindAnyObjectByType<Lantern>().gameObject.transform.localPosition;

            currentPosition = Utils.RandomPosition(
                LanternPosition,
                Data.Minigame.Game3.Cat.MIN_LIGHT_DISTANCE,
                Data.Minigame.Game3.Cat.MAX_LIGHT_DISTANCE,
                Data.Minigame.Game3.Cat.MIN_X_SPAWN,
                Data.Minigame.Game3.Cat.MAX_X_SPAWN,
                Data.Minigame.Game3.Cat.MIN_Y_SPAWN,
                Data.Minigame.Game3.Cat.MAX_Y_SPAWN
                );

            this.transform.localPosition = currentPosition;

            FlipScaleCat();
        }

        void FlipScaleCat()
        {
            float randomX = UnityEngine.Random.value > 0.5f ? 1f : -1f;
            this.transform.localScale = new Vector3(randomX, 1, 1);
        }

        public void Shake()
        {
            this._shake.Play(
                Data.Minigame.Game3.Cat.Shake.SPEED,
                Data.Minigame.Game3.Cat.Shake.INTERVAL,
                Data.Minigame.Game3.Cat.Shake.DURATION);
        }

        public void CatReset()
        {
            MinigameManager.instance.minigame.Defeat();
            this._shake.Stop();
            this._hop.Stop();

            this.timer = Data.Minigame.Game3.Cat.CATCH_TIME;
            this.transform.localPosition = currentPosition;
        }

        public void Timer()
        {
            if (timer >= 0) timer -= Time.deltaTime;
        }

        public void Catched()
        {
            _shake.Stop();
            _hop.Play(
                Data.Minigame.Game3.Cat.Hop.SPEED,
                Data.Minigame.Game3.Cat.Hop.AMPLITUDE);

            AudioManager.instance.PlayEffect(Data.Minigame.Game3.Cat.CAT_CATCH_SOUND);

            MinigameManager.instance.minigame.Victory();
            this.spriteRenderer.sprite = sprites[1];
        }

        public void CatchedAnimation()
        {
            if (timer >= 2) timer = 0;

            timer += Time.deltaTime * 2;

            float x = timer > 1 ? -1 : 1;

            transform.localScale = new Vector3(x, 1, 1);
        }
    }

}