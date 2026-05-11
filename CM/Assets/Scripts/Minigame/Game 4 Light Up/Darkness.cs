using UnityEngine;

public class Darkness : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        // 1. Obtener las dimensiones de la cámara
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        // 2. Obtener el tamaño del sprite en unidades del mundo
        float spriteWidth = sr.sprite.bounds.size.x;
        float spriteHeight = sr.sprite.bounds.size.y;

        // 3. Calcular la escala necesaria
        Vector3 worldScale = transform.localScale;
        worldScale.x = worldScreenWidth / spriteWidth;
        worldScale.y = worldScreenHeight / spriteHeight;

        transform.localScale = worldScale;
    }
}
