using UnityEngine;
using Minigame;
using System;

public class PlayerControllerDrag : MonoBehaviour
{
    public virtual void OnEnable()
    {
        InputManager.instance.DragActions += DragEvent;
    }

    public virtual void OnDisable()
    {
        InputManager.instance.DragActions -= DragEvent;
    }


    public virtual void DragEvent(Vector3 vector)
    {
        throw new System.NotImplementedException();
    }
}