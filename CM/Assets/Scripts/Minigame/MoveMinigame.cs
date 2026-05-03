using UnityEngine;
using System;
using System.Collections;
using Minigame;

public class MoveMinigame : ConstantMove
{
    private void Awake()
    {
        direction = Vector3.up; // Dirección del movimiento
        distance = 10f;             // A + 10
        duration = 0.25f;
        MinigameManager.instance.Move += StartMove;
    }

    private void OnDestroy()
    {
        MinigameManager.instance.Move -= StartMove;
    }

    private void LateUpdate()
    {
        MinigameManager.instance.isMoving = this.isMoving;
    }
}
