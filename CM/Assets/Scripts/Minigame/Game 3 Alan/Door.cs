using Minigame.Game2;
using UnityEngine;
using ShakeAnimation;

namespace Minigame.Game3
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

            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Alan>().Hit();
                spriteRenderer.sprite = closeDoor;
                AudioManager.instance.PlayEffect("Door");
                shake.startPosition = this.transform.localPosition;
                shake.Play(25f, 0.05f, 0.5f);
            }
        }

        public void SetPaused(bool isPaused)
        {
            this.paused = isPaused;
        }
    }
}