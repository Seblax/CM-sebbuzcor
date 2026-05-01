using Minigame;
using ShakeAnimation;
using UnityEngine;
using UnityEngine.UI;

public class LiveUI : MonoBehaviour
{
    [SerializeField] private int heartIndex;
    private bool _heart = true;

    private Image _image;
    public Sprite[] _sprites;
    private int _lastSpriteIndex = -1;
    
    private float timer;
    public float amplitude = 2;

    private Shake shake;

    void Start()
    {
        _image = GetComponent<Image>();
        shake = GetComponent<Shake>();

        MinigameUIManager.instance.OnScoreChanged += UpdateHeartStatus;
        _heart = heartIndex >= GameManager.instance.GetLives;
        if (_heart) DisableHeart();
    }

    private void OnDestroy()
    {
        MinigameUIManager.instance.OnScoreChanged -= UpdateHeartStatus;
    }

    void Update()
    {
        if (!_heart) return;
        timer += Time.deltaTime;
        LifeAnimation();
    }

    // Esta función decide si este corazón específico debe "morir"
    private void UpdateHeartStatus(int currentLives)
    {
        // Si el índice de este corazón es mayor o igual a las vidas actuales, se elimina.
        // Ejemplo: Si quedan 2 vidas, el corazón con índice 2 (el tercero) se apaga.
        if (heartIndex >= currentLives && _heart)
        {
            RemoveLife();
        }
    }

    public void RemoveLife()
    {
        if (!this._heart) return;
        this._heart = false;
        DisableHeart();
        shake.Play(50, 10, 0.15f);
    }

    void LifeAnimation() {
        float y = Mathf.Sin((this.heartIndex * 0.25f) + Mathf.PI * timer) * amplitude;
        transform.localPosition += Vector3.up * y;

        int currentSpriteIndex = (Mathf.Repeat(Time.time, 0.5f) < 0.25f) ? 0 : 1;

        if (currentSpriteIndex != _lastSpriteIndex)
        {
            _image.sprite = _sprites[currentSpriteIndex];
            _lastSpriteIndex = currentSpriteIndex;
        }
    }

    void DisableHeart() {
        this._image.sprite = _sprites[2];
        shake.startPosition = this.gameObject.transform.localPosition;
        shake.startPosition.y = -50;
    }
}
