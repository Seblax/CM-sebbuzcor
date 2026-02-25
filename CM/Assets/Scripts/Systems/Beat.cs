using UnityEngine;
using UnityEngine.Events;

public class Beat : MonoBehaviour
{
<<<<<<< HEAD
    public float _timer;
    public float _secondsPerBeat;
=======
    [SerializeField]
    float _timer;
    float _secondsPerBeat;
>>>>>>> origin/main

    [Header("Mussic Sync Settings")]
    public float bpm = 135f;
    public int beat;
    public int offset;

    [Header("Event")]
    public UnityEvent _event;


<<<<<<< HEAD
    public void Start()
=======
    void Start()
>>>>>>> origin/main
    {
        _timer = -offset * (60f / bpm);
    }

<<<<<<< HEAD
    protected virtual void Update()
=======
    void Update()
>>>>>>> origin/main
    {
        if (_event == null) return;
        if (beat <= 0) return;

        _secondsPerBeat = 60f / (bpm/beat);

<<<<<<< HEAD
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
=======
        _timer += Time.deltaTime;

        if (_timer >= _secondsPerBeat)
        {
            _timer -= _secondsPerBeat;
            _event?.Invoke();
        }   
>>>>>>> origin/main
    }
}
