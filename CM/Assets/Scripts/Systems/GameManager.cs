using UnityEngine;
using Gamemanager;
using System;

public class GameManager : Singleton<GameManager>
{
    public static GameManager Instance { get; private set; }
    public float minigameDuration = 7f;

    public float minigameTimer;
    public int lives = 5;

    public Action<int> UpdateLives;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void LateUpdate()
    {
        Time.timeScale = Aceleration.GetScale;
    }
}
