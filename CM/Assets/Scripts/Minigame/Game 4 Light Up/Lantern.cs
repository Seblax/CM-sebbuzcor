using System;
using UnityEngine;

namespace Minigame.Game4
{
    public class Lantern : PlayerControllerDrag
    {
        public float baseSpeed = 5f;       // Velocidad inicial
        public float maxSpeed = 25f;      // Velocidad tope
        public float acceleration = 10f;  // Qué tan rápido aumenta la velocidad

        private float currentSpeed;       // Velocidad actual acumulada
        public Rigidbody2D rb;

        public Action<Vector3> OnPositionChange;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            currentSpeed = baseSpeed; // Inicializamos
        }

        public override void DragEvent(Vector3 worldPosition)
        {
            if (IsPaused || MinigameManager.instance.minigame.Win || MinigameManager.instance.minigame.IsTimerOver)
            {
                ResetSpeed();
                return;
            }

            MinigameManager.instance.minigame.Defeat();

            Move(worldPosition);
            OnPositionChange.Invoke(this.transform.position);
        }

        public void Move(Vector3 worldPosition)
        {

            Vector2 targetDirection = (Vector2)worldPosition - rb.position;
            float distance = targetDirection.magnitude;

            if (distance < 0.1f)
            {
                ResetSpeed();
                return;
            }

            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
            Vector2 desiredVelocity = targetDirection.normalized * currentSpeed;

            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, desiredVelocity, Time.deltaTime * 5f);
        }

        public void ResetSpeed()
        {
            currentSpeed = baseSpeed;
            rb.linearVelocity = Vector2.zero;
        }
    }
}