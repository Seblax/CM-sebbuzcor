using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void OnPress()
    {
        GameManager.instance.Reset();
        AudioManager.instance.PlayEffect("HeartBreak");

    }
}
