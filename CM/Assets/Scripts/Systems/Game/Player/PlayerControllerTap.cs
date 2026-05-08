using UnityEngine;
using Minigame;

public class PlayerControllerTap : MonoBehaviour
{
    public virtual void OnEnable()
    {
        InputManager.instance.TapActions += this.TapEvent;
    }

    public virtual void OnDisable()
    {
        InputManager.instance.TapActions = null;
    }

    public virtual void OnDestroy()
    {
        InputManager.instance.TapActions = null;
    }

    public virtual void TapEvent()
    {
        throw new System.NotImplementedException();
    }
}