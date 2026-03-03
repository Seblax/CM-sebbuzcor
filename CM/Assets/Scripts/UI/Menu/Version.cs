using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

/// Este script sirve para mostrar en pantalla la versión actual del
/// juego
public class Version : MonoBehaviour
{
    void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = "v" + Application.version;
    }
}