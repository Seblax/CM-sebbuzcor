using UnityEngine;
using Minigame;

public class PlayerControllerTap : MonoBehaviour
{
    private void Awake()
    {
        InputManager.instance.TapActions += TapEvent;

    }

    public virtual void TapEvent()
    {
        throw new System.NotImplementedException();
    }
}