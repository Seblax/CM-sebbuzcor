using Gamemanager;
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
        public AlanTrail trail;

        [SerializeField] bool paused = true;
        public bool IsPaused { get => paused; }

        public Action<float> OnAlanChange;

        private void Start()
        {
            trail = this.GetComponentInChildren<AlanTrail>();
            trail.Stop();

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

        public override void OnDisable()
        {
            base.OnDisable();
            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause -= SetPaused;
        }

        public override void DragEvent(Vector3 worldPosition)
        {
            if (paused || MinigameManager.instance.minigame.Win || MinigameManager.instance.minigame.IsTimerOver) {
                ResetSpeed();
                return;
            }

            MinigameManager.instance.minigame.Defeat();

            Move(worldPosition);
        }

        public void Move(Vector3 worldPosition)
        {

            Vector2 targetDirection = (Vector2)worldPosition - rb.position;
            float distance = targetDirection.magnitude;

            if (distance < 0.1f)
            {
                // Reducimos la velocidad gradualmente en lugar de cortarla en seco
                rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.deltaTime * 10f);
                return;
            }

            // Calculamos la velocidad objetivo
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
            Vector2 desiredVelocity = targetDirection.normalized * currentSpeed;

            // EL TRUCO: Interpolamos la velocidad actual hacia la deseada
            // El valor '5f' controla qué tan "pesado" o "flotante" se siente (menor = más flotante)
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, desiredVelocity, Time.deltaTime * 5f);
        }

        public void ResetSpeed()
        {
            currentSpeed = baseSpeed;
            rb.linearVelocity = Vector2.zero;
        }

        public void Hit()
        {
            trail.Stop();
            GameManager.instance.score += (int)(200 * Aceleration.Scale);
            ResetSpeed();
            MinigameManager.instance.minigame.Victory();
            this.GetComponentInChildren<SpriteRenderer>().enabled = false; // Desaparece al ser golpeado
            AudioManager.instance.PlayEffect("BalloonPop");

            Destroy(this.gameObject);
        }

        public void LateUpdate()
        {
            if (MinigameManager.instance.minigame.IsTimerOver) trail.Stop();
            OnAlanChange?.Invoke(MinigameManager.instance.minigame.TimerPercent);
        }

        public void SetPaused(bool isPaused)
        {
            if (!isPaused)
            {
                trail.Play(1.25f);
            }

            this.paused = isPaused;
        }
    }
}