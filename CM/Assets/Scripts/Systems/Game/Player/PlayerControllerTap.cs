using UnityEngine;
using Minigame;

public class PlayerControllerTap : MonoBehaviour
{
    public virtual void OnEnable()
    {
        InputManager.instance.TapActions += TapEvent;
    }

    public virtual void OnDisable()
    {
        InputManager.instance.TapActions -= TapEvent;
    }

    public virtual void TapEvent()
    {
        throw new System.NotImplementedException();
    }
}