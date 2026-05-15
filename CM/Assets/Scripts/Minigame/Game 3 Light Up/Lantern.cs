using System;
using UnityEngine;

namespace Minigame.Game3
{
    public class Lantern : PlayerControllerDrag
    {
        private float currentSpeed;
        public Rigidbody2D rb;

        public Action<Vector3> OnPositionChange;

        public GameObject LanternSpriteObject;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            currentSpeed = Data.Minigame.Game3.Lantern.BASE_SPEED;
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

            currentSpeed = Mathf.MoveTowards(currentSpeed, Data.Minigame.Game3.Lantern.MAX_SPEED, Data.Minigame.Game3.Lantern.ACCELERATION * Time.deltaTime);
            Vector2 desiredVelocity = targetDirection.normalized * currentSpeed;

            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, desiredVelocity, Time.deltaTime * 5f);
        }

        public void ResetSpeed()
        {
            currentSpeed = Data.Minigame.Game3.Lantern.BASE_SPEED;
            rb.linearVelocity = Vector2.zero;
        }

        private void LateUpdate()
        {
            if (MinigameManager.instance.minigame.IsTimerOver) {
                Destroy(LanternSpriteObject, 1f);
            }
        }
    }
}