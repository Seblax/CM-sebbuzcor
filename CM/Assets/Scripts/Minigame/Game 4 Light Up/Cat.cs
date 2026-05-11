using Minigame.Game2;
using UnityEngine;
using ShakeAnimation;

namespace Minigame.Game4
{
    public class Cat : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        [SerializeField] Sprite closeDoor, openDoor;
        private Shake shake;

        private bool paused = true;
        public bool IsPaused => paused;

        private void Start()
        {
            this.transform.localPosition = this.RandomDoorPosition();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsPaused) return;

            if (collision.gameObject.CompareTag("Player"))
            {
                spriteRenderer.sprite = closeDoor;
                AudioManager.instance.PlayEffect("Door");
                shake.startPosition = this.transform.localPosition;
                shake.Play(25f, 0.05f, 0.5f);
            }
        }

        Vector3 RandomDoorPosition()
        {
            Camera cam = Camera.main;
            Vector3 finalPosition;
            float currentDistance;
            float requiredMinDistance = 4.5f; // Tu distancia mínima absoluta
            float requiredMaxDistance = 8f; // Tu distancia máxima absoluta

            int maxAttempts = 50; // ¡Seguro de vida!
            int currentAttempt = 0;

            do
            {
                float spawnX = Random.Range(0.15f, 0.85f);
                float spawnY = Random.Range(0.15f, 0.85f);

                float distanceZ = Mathf.Abs(cam.transform.position.z - Vector3.zero.z);
                Vector3 spawnViewportPos = new Vector3(spawnX, spawnY, distanceZ);
                finalPosition = cam.ViewportToWorldPoint(spawnViewportPos);

                currentDistance = Vector3.Distance(finalPosition, Vector3.zero);

                Debug.Log($"Current Door distance: {currentDistance}");
                if (currentAttempt > maxAttempts)
                {
                    currentAttempt = 0;
                    requiredMinDistance -= 0.1f;
                    Debug.Log($"Límite de intentos alcanzado: {currentDistance}");
                }

                currentAttempt++;
            } while (currentDistance < requiredMinDistance || currentDistance > requiredMaxDistance);

            return finalPosition;
        }
    }
}