using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Configuration : MonoBehaviour
{

    [SerializeField] Slider[] _soundConfiguration;

    void Start()
    {
        _soundConfiguration = GetComponentsInChildren<Slider>();

        _soundConfiguration[0].value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        _soundConfiguration[1].value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        _soundConfiguration[2].value = PlayerPrefs.GetFloat("EffectVolume", 0.5f); ;
    }

    public void DeleteData()
    {
        AudioManager.instance.PlayEffect("gg");
        Score.Score.DeleteScore();
    }

    public void MasterVolume()
    {
        PlayerPrefs.SetFloat("MasterVolume", _soundConfiguration[0].value);
        UdpdateAudio();
    }

    public void MusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", _soundConfiguration[1].value);
        UdpdateAudio();
    }

    public void EffectVolume()
    {
        PlayerPrefs.SetFloat("EffectVolume", _soundConfiguration[2].value);
        UdpdateAudio();
    }

    public void UdpdateAudio()
    {
        AudioManager.instance.UpdateMixer(
            PlayerPrefs.GetFloat("MasterVolume", 0.5f),
            PlayerPrefs.GetFloat("MusicVolume", 0.5f),
            PlayerPrefs.GetFloat("EffectVolume", 0.5f));
    }
}
