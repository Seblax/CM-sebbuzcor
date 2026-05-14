using UnityEngine;

/// <summary>
/// Script para hacer saltar el gameobject en función de los parámetros configurados
/// </summary>
/// 

namespace Animation
{
    public class Hop : MonoBehaviour
    {
        [Header("Settings")]
        public float amplitude = 75f;      //  Amplitud del salto
        public float speed = 0.25f;         //  Velocidad del salto
        public Vector3 direction = Vector3.up;         //  Velocidad del salto

        bool _isPlaying = false;            //  Está saltando
        Vector3 _startPos;                  //  Posición inicial del objeto
        float _timer = 0;

        public bool IsHopping { get => _isPlaying; }


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
                Stop();
                return;
            }

            // El salto es una función seno, por lo que no es un salto como tal simulado con física
            // sino un salto "arreglado".
            float y = Mathf.Sin(progress * Mathf.PI) * amplitude; transform.localPosition = _startPos + direction * y;
        }

        /// <summary>
        /// Inicia el salto
        /// </summary>
        /// 
        public void Play()
        {
            Play(speed, amplitude, Vector3.up);
        }

        public void Play(float speed)
        {
            Play(speed, amplitude, Vector3.up);
        }

        public void Play(float speed, float amplitude)
        {
            Play(speed, amplitude, Vector3.up);
        }

        public void Play(float speed, float amplitude, Vector3 direction)
        {
            this.speed = speed;
            this.amplitude = amplitude;
            this.direction = direction;

            this.Reset();
            _isPlaying = true;
        }

        public void Stop()
        {
            transform.localPosition = _startPos;
            _isPlaying = false;
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
}