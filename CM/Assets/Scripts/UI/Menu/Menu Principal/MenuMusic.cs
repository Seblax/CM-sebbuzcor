using UnityEngine;

/// <summary>
/// Almacena la duración de la pista actual en la clase MenuData
/// </summary>
public class PrincipalMenuMusic : MonoBehaviour
{
    private AudioSource _audioSource;

    void Awake()
    {
        this._audioSource = GetComponentInChildren<AudioSource>();
        MenuData.MENUMUSIC_TIMER = 0;
    }

    private void Update()
    {
        MenuData.MENUMUSIC_TIMER = this._audioSource.time;
    }
}
