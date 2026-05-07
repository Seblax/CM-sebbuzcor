using UnityEngine;
using Minigame;
using System;

public class PlayerControllerDrag : MonoBehaviour
{
    public virtual void OnEnable()
    {
        InputManager.instance.DragActions += this.DragEvent;
    }

    public virtual void OnDisable()
    {
        InputManager.instance.DragActions -= this.DragEvent;
    }

    public virtual void OnDestroy()
    {
        InputManager.instance.DragActions -= this.DragEvent;
    }


    public virtual void DragEvent(Vector3 vector)
    {
        throw new System.NotImplementedException();
    }
}