using Minigame.Game2;
using UnityEngine;
using ShakeAnimation;

namespace Minigame.Game3
{
    public class Door : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        [SerializeField] Sprite closeDoor, openDoor;
        private Shake shake;

        private void Start()
        {
            shake = GetComponent<Shake>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = openDoor;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Alan>().Hit();
                spriteRenderer.sprite = closeDoor;
                AudioManager.instance.PlayEffect("Door");
                shake.startPosition = this.transform.localPosition;
                shake.Play(5f, 0.05f, 0.5f);
            }
        }
    }
}