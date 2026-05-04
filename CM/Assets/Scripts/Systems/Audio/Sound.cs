using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource audioSource;            //Componente AudioSource
    public bool destroy = false;                //para ver si puede ser destruido o no

    public AudioSource GetAudioSource { get => audioSource; }

    /// <summary>
    /// Añade una componente AudioSource al objeto y mnodifica los valores en función al scriptableObjetc SoundEffect
    /// </summary>
    /// <param name="sound"></param>

    public void Play(SoundEffect sound)
    {
        this.audioSource = this.gameObject.AddComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = sound.group;
        audioSource.clip = sound.GetRandom();
        audioSource.pitch = sound.GetPitch();

        audioSource.volume = sound.volume;
        audioSource.panStereo = sound.estereo;

        audioSource.Play();

        destroy = true;        //se puede destruir una vez que acabe el audio
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //se destruye el objeto cuando acabe el audio
        if (!audioSource.isPlaying && destroy)
        {
            Destroy(this.gameObject);
        }
    }
}