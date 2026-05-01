using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 0.25f;         //  Velocidad del salto
    public Vector3 axis = Vector3.forward;         //  Velocidad del salto
    public bool reverse = false;         //  Velocidad del salto
    public bool playingOnAwake = false;         //  Velocidad del salto

    bool _isPlaying = false;            //  Está saltando
    float _timer = 0;

    public bool IsRotating { get => _isPlaying; }


    void Start()
    {
        this.Reset();
        this._isPlaying = playingOnAwake;
    }

    void Update()
    {
        Rotating();
    }

    // #==========================#
    //      Funciones
    // #==========================#
    void Rotating()
    {
        if (!_isPlaying) return;   

        _timer += Time.deltaTime;

        if (reverse) axis *= -1;

        this.transform.Rotate(axis * speed * Time.deltaTime);
    }

    public void Play()
    {
        this.Reset();
        _isPlaying = true;
    }

    public void Reset()
    {
        _timer = 0;
        _isPlaying = false;
    }
}
