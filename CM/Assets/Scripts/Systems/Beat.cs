using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script que sincroniza un temporizador específico con un evento que se ejecuta periodicamente
///     - bpm: son los beat por minuto.
///     - offset: la cantidad de tiempo que tarda en iniciar.
///     - beat: cuantas veces por compás se ejecuta el evento.
///     
///     - unityEvent: El evento que se va a ejecutar por cada invocación.
///     
///     
/// </summary>
public class Beat : MonoBehaviour
{

    public float _timer;
    public float _secondsPerBeat;


    [Header("Mussic Sync Settings")]
    public float bpm = 135f;
    public int beat;
    public int offset;

    [Header("Event")]
    public UnityEvent unityEvent;


    public void Start()
    {
        _timer = -offset * (60f / bpm);
    }

    protected virtual void Update()
    {
        if (unityEvent == null) return; //Si no tiene evento, entonces no hace nada
        if (beat <= 0) return;          //Si no se ejecuta ninguna vez, entonces no hace nada

        _secondsPerBeat = 60f / (bpm/beat); //calculo de segundos por beat

        Timer();
        Event();
    }

    /// <summary>
    /// Timer que aumenta constantemente 
    /// </summary>
    void Timer()
    {
        _timer += Time.deltaTime;
    }

    /// <summary>
    /// Logica del evento
    /// </summary>
    void Event()
    {
        // Cada vez que el timer sea mayor que los segundos por beat, se invoca el evento y se reinicia el timer
        if (_timer >= _secondsPerBeat)
        {
            _timer = 0;
            unityEvent?.Invoke();
        }
    }
}
