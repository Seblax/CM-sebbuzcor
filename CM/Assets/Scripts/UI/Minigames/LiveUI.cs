using Minigame;
using Animation;
using UnityEngine;
using UnityEngine.UI;

public class LiveUI : MonoBehaviour
{
    [SerializeField] private int heartIndex;
    [SerializeField] private bool _heart = true;

    private Image _image;
    public Sprite[] _sprites;
    private int _lastSpriteIndex = -1;
    
    private float timer;

    private Shake shake;

    void Start()
    {
        _image = GetComponent<Image>();
        shake = GetComponent<Shake>();

        this.transform.localPosition.Set(this.transform.localPosition.x, -Data.Minigame.UI.Hearts.Y_OFFSET, this.transform.localPosition.z);

        MinigameUIManager.instance.OnLivesChanged += UpdateHeartStatus;
    }

    private void OnDestroy()
    {
        MinigameUIManager.instance.OnLivesChanged -= UpdateHeartStatus;
    }

    void Update()
    {
        if (!_heart) return;
        LifeAnimation();
    }


    // Esta función decide si este corazón específico debe "morir"
    private void UpdateHeartStatus(int currentLives, bool animation)
    {
        if (heartIndex > currentLives && _heart)
        {
            RemoveLife(animation);
        }
    }

    public void RemoveLife(bool animation)
    {
        if (!this._heart) return;

        this._heart = false;
        DisableHeart();

        shake.Play(50, 10, 0.15f);
        if (!animation) return;

        AudioManager.instance.PlayEffect(Data.Minigame.UI.Hearts.BROKEN_HEART_SOUND);
    }

    void LifeAnimation() {
        timer += Time.deltaTime;
        float y = Mathf.Sin((this.heartIndex * 0.25f) + Mathf.PI * timer) * Data.Minigame.UI.Hearts.AMPLITUDE;
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
        shake.startPosition.y = -Data.Minigame.UI.Hearts.Y_OFFSET;
    }
}
