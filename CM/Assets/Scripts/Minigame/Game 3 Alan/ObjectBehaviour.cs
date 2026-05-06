using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using System.Collections;
using UnityEngine;

namespace Minigame.Game3
{
    public class ObjectBehaviour : MonoBehaviour, IPausable
    {
        [SerializeField] private bool isPaused;
        [SerializeField] private GameObject Door, Alan;

        public bool IsPaused => isPaused;

        private void Update()
        {
            if(IsPaused) return;
            Alan.transform.localPosition = RandomPosition();
            Door.transform.localPosition = RandomPosition(Door.transform.localPosition);
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

        Vector3 RandomPosition()
        {
            Camera cam = Camera.main;

            float spawnX = Random.Range(0f,1f);
            float spawnY = Random.Range(0.15f, 0.85f);
;

            Vector3 spawnViewportPos = new Vector3(spawnX, spawnY, Mathf.Abs(cam.transform.position.z));
            return cam.ViewportToWorldPoint(spawnViewportPos);
        }

        Vector3 RandomPosition(Vector3 referencePosition)
        {
            Camera cam = Camera.main;
            Vector3 finalPosition;
            float minDistance = 5f;
            int maxAttempts = 10; // Para evitar bucles infinitos en espacios pequeños
            int attempts = 0;

            do
            {
                float spawnX = Random.Range(0.15f, 0.85f);
                float spawnY = Random.Range(0.15f, 0.85f);

                // Usamos la distancia Z absoluta entre la cámara y la posición de referencia
                // para que el punto se genere en el mismo plano que el objeto
                float distanceZ = Mathf.Abs(cam.transform.position.z - referencePosition.z);
                Vector3 spawnViewportPos = new Vector3(spawnX, spawnY, distanceZ);
                finalPosition = cam.ViewportToWorldPoint(spawnViewportPos);

                attempts++;
            }
            while (Vector3.Distance(finalPosition, referencePosition) < minDistance && attempts < maxAttempts);

            return finalPosition;
        }

        public void SetPaused(bool isPaused)
        {
            this.isPaused = isPaused;
        }
    }
}