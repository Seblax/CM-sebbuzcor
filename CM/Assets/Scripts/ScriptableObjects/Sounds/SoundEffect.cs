using Gamemanager;
using UnityEngine;
using UnityEngine.Audio;
[CreateAssetMenu(menuName = "ScriptableObjects/SoundEffect")]

public class SoundEffect : ScriptableObject
{
    //############################################################
    //#                     Sound Effects                        #
    //############################################################

    public AudioClip[] clips;
    public bool accerelable = true;
    [SerializeField, Range(-3f, 3f)] public float randomPitch = 0f;
    [SerializeField, Range(-3f, 3f)] public float globalPitch = 1f;
    public AudioMixerGroup group;
    public float volume = 1;
    public float estereo = 0;


    public AudioClip GetRandom()
    {
        AudioClip res = null;

        if (clips.Length > 0)
        {
            res = clips[Random.Range(0, clips.Length)];
        }

        return res;
    }

    public float GetPitch()
    {
        float variation = Random.Range(-randomPitch, randomPitch);

        if (!accerelable)
            return globalPitch + variation;

        return globalPitch + (Aceleration.Scale - 1f) + variation;
    }

    public void addNewClip(AudioClip c)
    {
        clips = new AudioClip[] { c };
    }
}