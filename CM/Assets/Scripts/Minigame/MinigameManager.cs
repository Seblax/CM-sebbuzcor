using System;
using UnityEngine;
using UnityEngine.Events;
using Gamemanager;

public class MinigameManager : Singleton<MinigameManager>
{

    public float minigameDuration = 7;
    public float minigameTimer;
    public Action<float> UpdateUI;

    public UnityEvent placeHolder;

    public void Awake()
    {
        minigameTimer = minigameDuration;
    }

    public void Update()
    {
        if(minigameTimer > 0) this.minigameTimer -= Time.deltaTime;
        if (this.UpdateUI != null) UpdateUI?.Invoke(this.minigameTimer / this.minigameDuration);
        if (minigameTimer < 0)
        {
            this.minigameTimer = minigameDuration;
            placeHolder.Invoke();
            Aceleration.SetScale = Aceleration.GetScale+0.25f;
            Debug.Log($"Actual Aceleration Scale: {Aceleration.GetScale}");

            /////////////////////// PLACEHODLER
            MiniGameUiManager.instance.UpdateUI();
        }
    }
}
