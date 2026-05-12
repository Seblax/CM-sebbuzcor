using UnityEngine;

namespace Minigame.Game2
{
    public class ObjectBehaviour : MonoBehaviour, IPausable
    {
        [SerializeField] private bool isPaused;
        [SerializeField] private GameObject Door, Alan;

        public bool IsPaused => isPaused;

        private void Update()
        {
            if (IsPaused) return;
            Alan.transform.localPosition = RandomAlanPosition();
            Door.transform.localPosition = RandomDoorPosition(Alan.transform.localPosition);
            Destroy(gameObject);
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

        Vector3 RandomAlanPosition()
        {
            Camera cam = Camera.main;

            float spawnX = Random.Range(0f, 1f);
            float spawnY = Random.Range(0.15f, 0.85f);
            ;

            Vector3 spawnViewportPos = new Vector3(spawnX, spawnY, Mathf.Abs(cam.transform.position.z));
            return cam.ViewportToWorldPoint(spawnViewportPos);
        }

        Vector3 RandomDoorPosition(Vector3 referencePosition)
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

                float distanceZ = Mathf.Abs(cam.transform.position.z - referencePosition.z);
                Vector3 spawnViewportPos = new Vector3(spawnX, spawnY, distanceZ);
                finalPosition = cam.ViewportToWorldPoint(spawnViewportPos);

                currentDistance = Vector3.Distance(finalPosition, referencePosition);

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

        public void SetPaused(bool isPaused)
        {
            this.isPaused = isPaused;
        }
    }
}