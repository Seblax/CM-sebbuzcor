using Gamemanager;
using UnityEngine;
using UnityEngine.Audio;
[CreateAssetMenu(menuName = "Scriptable Objects/SoundEffect")]

public class SoundEffect : ScriptableObject
{
    //############################################################
    //#                     Sound Effects                        #
    //############################################################

    public AudioClip[] clips;
    public bool accerrelable = true;
    public float pitch;
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
        if (!accerrelable) return Random.Range(1 - pitch, 1 + pitch);
        return Aceleration.Scale + Random.Range( - pitch,  + pitch);
    }

    public void addNewClip(AudioClip c)
    {
        clips = new AudioClip[] { c };
    }
}