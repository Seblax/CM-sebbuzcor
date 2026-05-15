using Animation;
using Minigame;
using UnityEngine;
using UnityEngine.UI;


namespace Minigame.UI
{
    public class UILive : MonoBehaviour
    {
        [SerializeField] private int heartIndex;
        [SerializeField] private bool _heart;

        private Image _image;
        private Sprite[] _sprites;
        private int _lastSpriteIndex = -1;

        [SerializeField] private float timer;
        private Shake shake;

        // Guardamos la posición inicial para que siempre oscile respecto a ella
        private Vector3 _basePosition;

        void Awake()
        {
            _sprites = Resources.LoadAll<Sprite>(Data.Minigame.UI.Hearts.HEART_SPRITES_PATH);

            _image = GetComponent<Image>();
            shake = GetComponent<Shake>();
            _heart = heartIndex <= GameManager.instance.GetLives;



            // Definimos la posición base inicial
            _basePosition = new Vector3(transform.localPosition.x, -Data.Minigame.UI.Hearts.Y_OFFSET, transform.localPosition.z);
            transform.localPosition = _basePosition;

            MinigameUIManager.instance.OnLivesChanged += UpdateHeartStatus;
            if (!_heart) DisableHeart();
        }

        private void OnDestroy()
        {
            if (MinigameUIManager.instance != null)
                MinigameUIManager.instance.OnLivesChanged -= UpdateHeartStatus;
        }

        void Update()
        {
            // Si ya no tiene vida, no tocamos la posición (el Shake se encarga si está activo)
            if (!_heart) return;
            LifeAnimation();
        }

        private void UpdateHeartStatus(int currentLives, bool animation)
        {
            // IMPORTANTE: Ajustamos el índice (usualmente los índices son 0, 1, 2 y las vidas 1, 2, 3)
            // Si heartIndex es 1-based, la lógica es:
            if (heartIndex > currentLives && _heart)
            {
                RemoveLife(animation);
            }
        }

        public void RemoveLife(bool animation)
        {
            this._heart = false;
            DisableHeart();

            if (!animation) return;

            // El shake ahora funcionará sobre la posición base reseteada
            shake.Play(50, 10, 0.15f);
            AudioManager.instance.PlayEffect(Data.Minigame.UI.Hearts.BROKEN_HEART_SOUND);
        }

        void LifeAnimation()
        {
            timer += Time.deltaTime;

            // Calculamos el desplazamiento de forma absoluta
            float offset = Mathf.Sin((this.heartIndex * 0.25f) + Mathf.PI * timer) * Data.Minigame.UI.Hearts.AMPLITUDE;

            // Aplicamos el desplazamiento SIEMPRE sumando a la posición BASE, no a la actual
            transform.localPosition = _basePosition + (Vector3.up * offset);

            // Cambio de sprite (Optimizado)
            int currentSpriteIndex = (Mathf.Repeat(Time.time, 0.5f) < 0.25f) ? 0 : 1;
            if (currentSpriteIndex != _lastSpriteIndex)
            {
                _image.sprite = _sprites[currentSpriteIndex];
                _lastSpriteIndex = currentSpriteIndex;
            }
        }

        void DisableHeart()
        {
            this._image.sprite = _sprites[2];

            // Forzamos que vuelva a la posición base antes de que el Shake tome el control
            transform.localPosition = _basePosition;

            // Si tu script de Shake usa una posición de inicio, asegúrate de que sea la base
            shake.startPosition = _basePosition;
        }
    }
}