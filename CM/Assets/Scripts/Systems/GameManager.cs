using UnityEngine;
using Gamemanager;
using System;

public class GameManager : Singleton<GameManager>
{
    public float minigameDuration = 7f;

    public float minigameTimer;
    public int lives = 5;
    public int score = 0;

    public int rounds = 0; 
    public int GetCurrentRound { get => rounds; }
    public int IncrementRound { set => rounds += 1; }
    public int GetLives { get => lives; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void LateUpdate()
    {
        Time.timeScale = Aceleration.GetScale;
    }
}