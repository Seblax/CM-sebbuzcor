using UnityEngine;
using UnityEngine.Events;

public class Beat : MonoBehaviour
{
    [SerializeField]
    float _timer;
    float _secondsPerBeat;

    [Header("Mussic Sync Settings")]
    public float bpm = 135f;
    public int beat;
    public int offset;

    [Header("Event")]
    public UnityEvent _event;


    void Start()
    {
        _timer = -offset * (60f / bpm);
    }

    void Update()
    {
        if (_event == null) return;
        if (beat <= 0) return;

        _secondsPerBeat = 60f / (bpm/beat);

        _timer += Time.deltaTime;

        if (_timer >= _secondsPerBeat)
        {
            _timer -= _secondsPerBeat;
            _event?.Invoke();
        }   
    }
}
