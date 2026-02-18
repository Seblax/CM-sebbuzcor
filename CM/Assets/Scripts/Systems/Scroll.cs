using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    RawImage _rawImage;
    [SerializeField]
    
    public bool inverse;
    public float speed = 1;

    int direction = 1;
    Vector2 _offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rawImage = this.gameObject.GetComponent<RawImage>();
        direction = inverse ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        _offset += Vector2.right * direction * speed * Time.deltaTime;
        _rawImage.uvRect = new Rect(_offset, _rawImage.uvRect.size);
    }

}
