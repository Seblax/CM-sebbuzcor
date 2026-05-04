using ShakeAnimation;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] Image mute;
    [SerializeField] private Sprite[] muteSprites;
    [SerializeField] Shake shake;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        mute.sprite = AudioManager.instance.IsMuted ?
             muteSprites[1] : muteSprites[0];
    }

    // Update is called once per frame
    public void OnPress()
    {
        AudioManager.instance.Mute();
        AudioManager.instance.PlayEffect("Button");
        mute.sprite = AudioManager.instance.IsMuted ?
             muteSprites[1] : muteSprites[0];

        shake.Play(2f,0.15f,0.25f);
    }
}
