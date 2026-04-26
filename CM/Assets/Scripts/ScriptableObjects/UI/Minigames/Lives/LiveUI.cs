using ShakeAnimation;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.UI;

public class LiveUI : MonoBehaviour
{
    private Image _image;
    [SerializeField]
    private bool _heart = true;
    private float timer;
    private float offset;
    private Shake shake;

    public Sprite[] _sprites;
    public float amplitude = 2;

    void Start()
    {
        _image = GetComponent<Image>();
        offset = Random.Range(0,1f);
        shake = gameObject.GetComponent<Shake>();
    }

    // Update is called once per frame
    void Update()
    {
        //Placeholder
        if (timer > 3 * offset) {
            RemoveLife();
        }

        if (!_heart) return;
        timer += Time.deltaTime;

        LifeAnimation();
    }

    void LifeAnimation() {
        float y = Mathf.Sin(offset + Mathf.PI * timer) * amplitude; 
        transform.localPosition += Vector3.up * y;
        if (Mathf.Repeat(Time.time, 0.5f) < 0.25f)
        {
            this._image.sprite = _sprites[0];
        }
        else
        {
            this._image.sprite = _sprites[1];
        }
    }

    public void RemoveLife()
    {
        if (!this._heart) return;

        this._heart = false;
        this._image.sprite = _sprites[2];
        shake.startPosition = this.gameObject.transform.localPosition;
        shake.startPosition.y = -50;
        shake.Play(50, 10, 0.15f);
    }
}
