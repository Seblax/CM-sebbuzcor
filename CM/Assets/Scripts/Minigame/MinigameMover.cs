using UnityEngine;
using Minigame;

namespace Minigame
{
    public class MinigameMover : ConstantMove
    {
        private void Awake()
        {
            direction = Vector3.up; // Dirección del movimiento
            distance = 10f;             // A + 10
            duration = 0.25f;
            MinigameManager.instance.SetMover = this;
        }
    }
}