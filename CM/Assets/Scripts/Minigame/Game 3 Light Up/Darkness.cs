using UnityEngine;

public class Darkness : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        float spriteWidth = sr.sprite.bounds.size.x;
        float spriteHeight = sr.sprite.bounds.size.y;

        Vector3 worldScale = transform.localScale;
        worldScale.x = worldScreenWidth / spriteWidth;
        worldScale.y = worldScreenHeight / spriteHeight;

        transform.localScale = worldScale;
    }
}
