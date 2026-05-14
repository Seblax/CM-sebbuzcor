using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;

public class AudioManager : Singleton<AudioManager>
{
    private const string MASTER_PARAM = "MasterVolume";
    private const string MUSIC_PARAM = "MusicVolume";
    private const string EFFECTS_PARAM = "EffectVolume";

    public SoundEffect[] effects;                                  //Todos los scripotables objects

    private Dictionary<string, SoundEffect> _effectDictionary;     //memoria de scirptable objects y nombre

    private AudioListener _listener;

    private AudioMixer audioMixer;

    private bool mute;

    public bool IsMuted => mute;

    private void Awake()
    {
        effects = Resources.LoadAll<SoundEffect>("");                  //se cargan absolutamente todos los SoundEffects

        audioMixer = Resources.Load<AudioMixer>("Sounds/Mixer");

        _effectDictionary = new Dictionary<string, SoundEffect>();     //se Inicializa el diccionario

        //Por cada SoundEffect en el Array SoundEffects, se registrará su nombre y se añadirá al diccionario
        foreach (var effect in effects)
        {
            //si no está ya en el diccionario se le añade
            if (!_effectDictionary.ContainsKey(effect.name))
            {
                Debug.LogFormat("registered effect {0}", effect.name);
                _effectDictionary[effect.name] = effect;
            }
        }

    }

    public void PlayEffect(SoundEffect effect)
    {
        PlayEffect(effect.name);
    }

    //PlayEffect sirve para ejecutar el sonido
    public Sound PlayEffect(string effectName)
    {
        //busca el audioListener
        if (_listener == null)
        {
            _listener = FindFirstObjectByType<AudioListener>();
        }

        //crea el objeto en funcion de donde esté el audioListener
        return PlayEffect(effectName, _listener.transform.position);
    }

    public Sound PlayEffect(string effectName, Vector3 position)
    {

        //comprobamos que el sonido existe en la memoria
        if (!_effectDictionary.ContainsKey(effectName))
        {
            Debug.LogWarningFormat($"Effect {effectName} not found");
            return null;
        }

        //obtenemos el sonido de la memoria
        SoundEffect sound = _effectDictionary[effectName];

        //Comprobamos qu eel scriptable objets contenga sonidos
        if (sound.GetRandom() == null)
        {
            Debug.LogWarningFormat($"Effect {effectName} has no clip to play");
            return null;
        }

        //se creará un nuevo GameObject que reproducirá el sonido y se eliminará usando AudioSourceGenerator
        GameObject effectSound = new GameObject("Sonido de " + effectName);
        Sound s = effectSound.AddComponent<Sound>();
        s.Play(sound);
        return s;
    }

    /// <summary>
    /// Crea sonido a través de un Audio CLip
    /// </summary>
    /// <param name="clip"></param>
    public Sound PlayEffect(AudioClip clip)
    {
        SoundEffect sound = new SoundEffect();

        sound.addNewClip(clip);
        sound.name = clip.name;

        if (sound.GetRandom() == null)
        {
            Debug.LogWarningFormat($"Effect {sound.name} has no clip to play");
            return null;
        }

        //se creará un nuevo GameObject que reproducirá el sonido y se eliminará usando AudioSourceGenerator
        GameObject effectSound = new GameObject("Sonido de " + sound.name);
        Sound s = effectSound.AddComponent<Sound>();
        s.Play(sound);
        return s;
    }

    public void PlayEffectDelay(SoundEffect effect, float delay)
    {
        StartCoroutine(PlayEffectWithDelay(effect, delay));
    }

    public void PlayEffectDelay(string effect, float delay)
    {
        StartCoroutine(PlayEffectWithDelay(effect, delay));
    }

    private IEnumerator PlayEffectWithDelay(string effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayEffect(effect);
    }

    private IEnumerator PlayEffectWithDelay(SoundEffect effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayEffect(effect);
    }

    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        Debug.Log($"Start Fade in {audioSource}");
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
    }

    public void UpdateMixer(float master, float music, float effect)
    {
        // Convertimos el valor 0-100 a Decibelios y aplicamos al Mixer
        audioMixer.SetFloat(MASTER_PARAM, ConvertToDecibel(master));
        audioMixer.SetFloat(MUSIC_PARAM, ConvertToDecibel(music));
        audioMixer.SetFloat(EFFECTS_PARAM, ConvertToDecibel(effect));
    }

    public void Mute()
    {
        mute = !mute;

        if (IsMuted)
        {
            audioMixer.SetFloat(MASTER_PARAM, ConvertToDecibel(0));
        }
        else
        {
            audioMixer.SetFloat(MASTER_PARAM, ConvertToDecibel(PlayerPrefs.GetFloat("MasterVolume", 0.5f)));
        }

    }

    private float ConvertToDecibel(float value)
    {
        float volume = Mathf.Clamp(value, 0.0001f, 1f);

        return Mathf.Log10(volume) * 20f;
    }
}