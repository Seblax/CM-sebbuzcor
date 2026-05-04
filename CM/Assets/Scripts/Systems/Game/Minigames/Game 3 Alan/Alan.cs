using ShakeAnimation;
using System;
using UnityEngine;

namespace Minigame.Game2
{
    public class Alan : PlayerControllerDrag, IPausable
    {
        public float baseSpeed = 5f;       // Velocidad inicial
        public float maxSpeed = 25f;      // Velocidad tope
        public float acceleration = 10f;  // Qué tan rápido aumenta la velocidad

        private float currentSpeed;       // Velocidad actual acumulada
        public Rigidbody2D rb;
        public TrailRenderer trail;

        [SerializeField] bool paused = true;
        public bool IsPaused { get => paused; }

        private void Start()
        {
            trail = this.GetComponentInChildren<TrailRenderer>(); 
            trail.enabled = false; // Desactivamos el trail al inicio

            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0; // Aseguramos que no caiga por gravedad

            currentSpeed = baseSpeed; // Inicializamos
        }

        public override void OnEnable()
        {
            base.OnEnable();
            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause += SetPaused;
        }

        // Se asume que DragEvent se llama constantemente mientras arrastras
        public override void DragEvent(Vector3 worldPosition) // Ahora recibe posición del mundo
        {
            if (paused) return;
            if (MinigameManager.instance.minigame.Win) return;

            MinigameManager.instance.minigame.Defeat();
            trail.enabled = true; // Desactivamos el trail al inicio

            // 1. Dirección: (Hacia donde voy - Donde estoy)
            Vector2 direction = (Vector2)worldPosition - rb.position;

            // Si estamos muy cerca del dedo, paramos para evitar que "vibre"
            if (direction.magnitude < 0.1f)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            // 2. Aceleración constante mientras se arrastra
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, baseSpeed, maxSpeed);

            // 3. Aplicar velocidad
            rb.linearVelocity = direction.normalized * currentSpeed;
        }

        // Opcional: Si quieres que la velocidad se resetee al soltar el clic
        // deberías sobrescribir el evento de "EndDrag" o similar en tu PlayerControllerDrag
        public void ResetSpeed()
        {
            currentSpeed = baseSpeed;
            rb.linearVelocity = Vector2.zero;
        }

        public void Hit()
        {
            trail.enabled = false;
            GameManager.instance.score -= 200;
            ResetSpeed();
            MinigameManager.instance.minigame.Victory();
            this.GetComponent<SpriteRenderer>().enabled = false; // Desaparece al ser golpeado
            AudioManager.instance.PlayEffect("BaloonPop"); // Asegúrate de tener un sonido llamado "BaloonPop" en tu AudioManager
            Destroy(this.gameObject);
        }

        public void LateUpdate()
        {
            if(MinigameManager.instance.minigame.IsTimerOver) Destroy(this);
        }

        public void SetPaused(bool isPaused)
        {
            this.paused = isPaused;
        }
    }
}