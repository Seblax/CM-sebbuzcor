using Minigame.Game2;
using UnityEngine;
using ShakeAnimation;

namespace Minigame.Game2
{
    public class Door : MonoBehaviour, IPausable
    {
        SpriteRenderer spriteRenderer;
        [SerializeField] Sprite closeDoor, openDoor;
        private Shake shake;

        private bool paused = true;
        public bool IsPaused => paused;

        private void Start()
        {
            shake = GetComponent<Shake>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = openDoor;
        }

        public void OnEnable()
        {
            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause += SetPaused;
        }

        public void OnDisable()
        {
            if (MinigameManager.instance != null)
                MinigameManager.instance.Pause -= SetPaused;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsPaused) return;

            if (collision.gameObject.CompareTag(Data.Minigame.PLAYER_TAG))
            {
                collision.gameObject.GetComponent<Alan>().Hit();

                spriteRenderer.sprite = closeDoor;
                
                AudioManager.instance.PlayEffect(Data.Minigame.Game2.Door.DOOR_SOUND);
                
                shake.startPosition = this.transform.localPosition;
                shake.Play(
                    Data.Minigame.Game2.Door.Shake.SPEED,
                    Data.Minigame.Game2.Door.Shake.INTERVAL,
                    Data.Minigame.Game2.Door.Shake.DURATION);
            }
        }

        public void SetPaused(bool isPaused)
        {
            this.paused = isPaused;
        }

        public void SetDoorPosition(Vector3 vector)
        {
            this.transform.localPosition = Utils.RandomPosition(
                    vector,
                    Data.Minigame.Game2.Door.MIN_ALAN_DISTANCE,
                    Data.Minigame.Game2.Door.MAX_ALAN_DISTANCE,
                    Data.Minigame.Game2.Door.MIN_X_SPAWN,
                    Data.Minigame.Game2.Door.MAX_X_SPAWN,
                    Data.Minigame.Game2.Door.MIN_Y_SPAWN,
                    Data.Minigame.Game2.Door.MAX_Y_SPAWN
                );
        }

    }
}