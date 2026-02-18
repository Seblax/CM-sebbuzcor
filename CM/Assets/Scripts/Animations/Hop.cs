using UnityEngine;

public class Hop : MonoBehaviour
{
    [Header("Settings")]
    public float _amplitude = 75f;
    public float speed = 0.25f;

    bool _isPlaying = false;
    Vector3 _startPos;
    float _timer = 0;

    void Start()
    {
        this.Reset();
    }

    void Update()
    {
        OnBeat();
    }

    void OnBeat()
    {
        if (!_isPlaying) return;

        _timer += Time.deltaTime;

        float progress = _timer / speed;

        if (progress >= 1f)
        {
            transform.localPosition = _startPos;
            _isPlaying = false;
            return;
        }

        float y = Mathf.Sin(progress * Mathf.PI) * _amplitude; transform.localPosition = _startPos + Vector3.up * y;
    }

    public void Play()
    {
        this.Reset();
        _isPlaying = true;
    }

    public void Reset()
    {
        _startPos = transform.localPosition;
        _timer = 0;
        _isPlaying = false;
    }

}
