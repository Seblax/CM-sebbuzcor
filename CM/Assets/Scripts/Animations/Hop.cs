using UnityEngine;

/// <summary>
/// Script para hacer saltar el gameobject en función de los parámetros configurados
/// </summary>
public class Hop : MonoBehaviour
{
    [Header("Settings")]
    public float _amplitude = 75f;      //  Amplitud del salto
    public float speed = 0.25f;         //  Velocidad del salto

    bool _isPlaying = false;            //  Está saltando
    Vector3 _startPos;                  //  Posición inicial del objeto
    float _timer = 0;                   


    void Start()
    {
        this.Reset();
    }

    void Update()
    {
        Hopping();
    }

    // #==========================#
    //      Funciones
    // #==========================#

    /// <summary>
    /// Función que controla la lógica del salto.
    /// </summary>
    void Hopping()
    {
        if (!_isPlaying) return;    //Si ya está saltando, no se ejecuta nada

        _timer += Time.deltaTime;

        float progress = _timer / speed;    //  Tiempo que va a tardar en hacer el salto

        if (progress >= 1f)                 //  Si ya ha dado el salto, se reinicia
        {
            transform.localPosition = _startPos;
            _isPlaying = false;
            return;
        }

        // El salto es una función seno, por lo que no es un salto como tal simulado con física
        // sino un salto "arreglado".
        float y = Mathf.Sin(progress * Mathf.PI) * _amplitude; transform.localPosition = _startPos + Vector3.up * y;
    }

    /// <summary>
    /// Inicia el salto
    /// </summary>
    public void Play()
    {
        this.Reset();
        _isPlaying = true;
    }

    /// <summary>
    /// Reinicia las variables
    /// </summary>
    public void Reset()
    {
        _startPos = transform.localPosition;
        _timer = 0;
        _isPlaying = false;
    }

}
