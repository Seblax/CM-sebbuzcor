using Gamemanager;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Minigame.Game2
{
    public class Alan : PlayerControllerDrag
    {
        private float currentSpeed;
        public Rigidbody2D rb;
        public AlanTrail trail;

        public Action<float> OnAlanChange;

        private void Start()
        {
            trail = this.GetComponentInChildren<AlanTrail>();
            trail.Stop();

            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            currentSpeed = Data.Minigame.Game2.Alan.BASE_SPEED;

            this.transform.localPosition = Utils.RandomPosition(
                    Data.Minigame.Game2.Alan.MIN_X_SPAWN,
                    Data.Minigame.Game2.Alan.MAX_X_SPAWN,
                    Data.Minigame.Game2.Alan.MIN_Y_SPAWN,
                    Data.Minigame.Game2.Alan.MIN_Y_SPAWN
            );

            FindAnyObjectByType(typeof(Door)).GetComponent<Door>().SetDoorPosition(this.transform.localPosition);
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
        }

        public void Move(Vector3 worldPosition)
        {

            Vector2 targetDirection = (Vector2)worldPosition - rb.position;
            float distance = targetDirection.magnitude;

            if (distance < 0.1f)
            {
                rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.deltaTime * 10f);
                return;
            }

            // Calculamos la velocidad objetivo

            float maxSpeed = Data.Minigame.Game2.Alan.MAX_SPEED;
            float acceleration = Data.Minigame.Game2.Alan.ACCELERATION;

            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);

            Vector2 desiredVelocity = targetDirection.normalized * currentSpeed;

            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, desiredVelocity, Time.deltaTime * 5f);
        }

        public void ResetSpeed()
        {
            currentSpeed = Data.Minigame.Game2.Alan.BASE_SPEED;
            rb.linearVelocity = Vector2.zero;
        }

        public void Hit()
        {
            AudioManager.instance.PlayEffect(Data.Minigame.Game2.Alan.BALLOON_POP_SOUND);
            trail.Stop();
            ResetSpeed();

            this.GetComponentInChildren<SpriteRenderer>().enabled = false;

            Destroy(this.gameObject);

            GameManager.instance.score += (int)(Data.Minigame.Game2.Alan.BASE_SCORE * Aceleration.Scale);
            MinigameManager.instance.minigame.Victory();
        }

        public void LateUpdate()
        {
            if (MinigameManager.instance.minigame.IsTimerOver) trail.Stop();
            OnAlanChange?.Invoke(MinigameManager.instance.minigame.TimerPercent);
        }

        public override void SetPaused(bool isPaused)
        {
            base.SetPaused(isPaused);
            if (!isPaused)
            {
                trail.Play(Data.Minigame.Game2.Alan.TRAIL_DELAY);
            }
        }
    }
}