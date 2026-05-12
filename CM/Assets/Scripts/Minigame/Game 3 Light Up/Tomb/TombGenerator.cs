using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Game3
{
    public class TombGenerator : MonoBehaviour
    {
        int tombNumber;
        [SerializeField] GameObject tombPrefab;
        public List<Vector3> tombPositions;

        void Start()
        {
            tombNumber = Random.Range(3, 7);

            while (tombNumber > 0)
            {
                GenerateTombs();
                tombNumber--;
            }

        }

        void GenerateTombs()
        {
            Vector3 tombPosition = SetTombRandomPosition();

            GameObject newTomb = Instantiate(tombPrefab, transform);

            newTomb.transform.localPosition = tombPosition;

            float randomX = UnityEngine.Random.value > 0.5f ? 1f : -1f;
            newTomb.transform.localScale = new Vector3(randomX, 1, 1);

            tombPositions.Add(tombPosition);
        }

        Vector3 SetTombRandomPosition()
        {
            Camera cam = Camera.main;
            Vector3 res = Vector3.zero;
            bool positionValid = false;
            int attempts = 0; // Para evitar bucles infinitos si no hay espacio

            while (!positionValid && attempts < 100)
            {
                float spawnX = Random.Range(0.1f, 0.90f);
                float spawnY = Random.Range(0.15f, 0.85f);

                Vector3 spawnViewportPos = new Vector3(spawnX, spawnY, Mathf.Abs(cam.transform.position.z));
                res = cam.ViewportToWorldPoint(spawnViewportPos);

                // Comprobar distancia con las tumbas existentes
                positionValid = true;
                foreach (Vector3 pos in tombPositions)
                {
                    if (Vector3.Distance(res, pos) < 2f)
                    {
                        positionValid = false;
                        break;
                    }
                }
                attempts++;
            }

            return res;
        }
    }
}