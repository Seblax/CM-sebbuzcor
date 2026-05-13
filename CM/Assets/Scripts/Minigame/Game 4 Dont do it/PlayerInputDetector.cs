using Minigame;
using System;
using UnityEngine;

public class PlayerInputDetector : PlayerController
{
    public Action PlayerInputDetected;

    private void Start()
    {
        MinigameManager.instance.minigame.Victory();
    }

    public override void TapEvent()
    {
        if (IsPaused) return;
        MinigameManager.instance.minigame.Defeat();
        PlayerInputDetected?.Invoke();
    }

    public override void DragEvent(Vector3 worldPos)
    {
        if (IsPaused) return;
        MinigameManager.instance.minigame.Defeat();
        PlayerInputDetected?.Invoke();
    }
}
