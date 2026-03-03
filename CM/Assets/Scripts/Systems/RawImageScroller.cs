using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]

/// Scrollea la textura del componente raw image a una velocidad especificada de forma indefinida
public class RawImageScroller : MonoBehaviour
{
    RawImage _rawImage;
    public bool inverse;
    public float speed = 1;

    int direction = 1;
    Vector2 _offset;

    void Awake()
    {
        _rawImage = this.gameObject.GetComponent<RawImage>();
        direction = inverse ? -1 : 1;
    }

    void Update()
    {
        _offset += Vector2.right * direction * speed * Time.deltaTime;
        _rawImage.uvRect = new Rect(_offset, _rawImage.uvRect.size);
    }

}
