using UnityEngine;
using UnityEngine.Events;

public class Beat : MonoBehaviour
{
    public float _timer;
    public float _secondsPerBeat;

    [Header("Mussic Sync Settings")]
    public float bpm = 135f;
    public int beat;
    public int offset;

    [Header("Event")]
    public UnityEvent _event;


    public void Start()
    {
        _timer = -offset * (60f / bpm);
    }

    protected virtual void Update()
    {
        if (_event == null) return;
        if (beat <= 0) return;

        _secondsPerBeat = 60f / (bpm/beat);

        Timer();
        Event();
    }

    protected virtual void Timer()
    {
        _timer += Time.deltaTime;
    }

    protected virtual void Event()
    {
        if (_timer >= _secondsPerBeat)
        {
            _timer = 0;
            _event?.Invoke();
        }
    }
}
