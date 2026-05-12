using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class TombGenerator : MonoBehaviour
{
    int tombNumber;

    public Sprite[] tombSprites;
    public List<Vector3> tombPositions;

    void Start()
    {
        tombNumber = Random.Range(3,7);

        while (tombNumber > 0) { 
            GenerateTombs();
            tombNumber--;
        }

    }

    void GenerateTombs() {
        GameObject tombs = new GameObject();

        SpriteRenderer spriteRender = tombs.AddComponent<SpriteRenderer>();
        
        spriteRender.sprite = tombSprites.GetRandom();
        spriteRender.sortingOrder = 1;
        Vector3 tombPosition = SetTombRandomPosition();

        tombs.transform.SetParent(transform, false);
        tombs.transform.localPosition = tombPosition;
        tombs.transform.localScale *= Random.Range(1f, 2f);
        
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
