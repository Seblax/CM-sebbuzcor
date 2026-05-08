using UnityEngine;
using Minigame;
using System;

public class PlayerControllerDrag : MonoBehaviour, IPausable
{
    [SerializeField] bool paused = true;
    public bool IsPaused { get => paused; }

    public virtual void OnEnable()
    {
        InputManager.instance.DragActions += this.DragEvent;

        if (MinigameManager.instance != null)
            MinigameManager.instance.Pause += SetPaused;
    }

    public virtual void OnDisable()
    {
        InputManager.instance.DragActions -= null;

        if (MinigameManager.instance != null)
            MinigameManager.instance.Pause -= SetPaused;
    }

    public virtual void DragEvent(Vector3 vector)
    {
        throw new System.NotImplementedException();
    }

    public virtual void SetPaused(bool isPaused)
    {
        paused = isPaused;
    }
}