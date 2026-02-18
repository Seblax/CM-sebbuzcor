using TMPro;
using UnityEngine;

public class Version : MonoBehaviour
{
    TextMeshProUGUI _text;

    void Start()
    {
        this._text = GetComponent<TextMeshProUGUI>();
        _text.text = Application.version + "v";
    }
}
