using UnityEngine;
using Minigame;

public class PlayerControllerTap : MonoBehaviour, IPausable
{

    [SerializeField] bool paused = true;
    public bool IsPaused { get => paused; }

    public virtual void OnEnable()
    {
        Debug.Log("PlayerControllerTap enabled");

        InputManager.instance.TapActions += this.TapEvent;

        if (MinigameManager.instance != null)
            MinigameManager.instance.Pause += SetPaused;
    }

    public virtual void OnDisable()
    {
        InputManager.instance.TapActions -= this.TapEvent;
        
        if (MinigameManager.instance != null)
            MinigameManager.instance.Pause -= SetPaused;
    }

    public virtual void TapEvent()
    {
        throw new System.NotImplementedException();
    }

    public virtual void SetPaused(bool isPaused)
    {
        paused = isPaused;
    }
}