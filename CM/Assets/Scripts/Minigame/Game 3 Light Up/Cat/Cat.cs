using ShakeAnimation;
using UnityEngine;



namespace Minigame.Game3
{

    public class Cat : MonoBehaviour
    {
        private readonly string CAT_SPRITES_PATH = "Textures/Minigame/Game 3/gato";

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] sprites;

        Shake _shake;
        Hop _hop;

        [SerializeField] private float timer;
        [SerializeField] private float catchTime = 1.5f;
        [SerializeField] private Vector3 currentPosition;

        public bool Shaking => _shake.isPlaying;
        public bool IsCatched => timer <= 0;

        void Awake()
        {
            this.sprites = Resources.LoadAll<Sprite>(CAT_SPRITES_PATH);

            this.spriteRenderer = GetComponent<SpriteRenderer>();
            this._shake = GetComponent<Shake>();
            this._hop = GetComponent<Hop>();

            this.spriteRenderer.sprite = sprites[0];
        }

        public void SetRandomPosition()
        {
            Camera cam = Camera.main;

            float spawnX = Random.Range(0.1f, 0.90f);
            float spawnY = Random.Range(0.15f, 0.85f);
            ;

            Vector3 spawnViewportPos = new Vector3(spawnX, spawnY, Mathf.Abs(cam.transform.position.z));
            currentPosition = cam.ViewportToWorldPoint(spawnViewportPos);
            this.transform.localPosition = currentPosition;
        }

        public void Shake()
        {
            this._shake.Play(5, 0.15f, 3.5f);
        }

        public void CatReset()
        {
            MinigameManager.instance.minigame.Defeat();
            this._shake.Stop();
            this._hop.Stop();

            this.timer = catchTime;
            this.transform.localPosition = currentPosition;
        }

        public void Timer()
        {
            if (timer >= 0) timer -= Time.deltaTime;
        }

        public void Catched()
        {
            _hop.Play(0.25f, 1.5f);
            AudioManager.instance.PlayEffect("CatCatch");
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