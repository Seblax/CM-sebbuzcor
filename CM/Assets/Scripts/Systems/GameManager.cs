using UnityEngine;
using Gamemanager;
using Minigame;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GameManager : Singleton<GameManager>
{
    public readonly string MINIGAME_GAMEOBJECT_PATHS = "Prefabs/Minigames/";
    public GameObject[] minigamePrefabs;

    public float minigameDuration = 7f;

    public float minigameTimer;
    public int lives = 3;
    public int score = 0;


    public int rounds = 0;
    public int roundsIncrementation = 3;
    public int GetCurrentRound
    {
        get
        {
            Debug.Log($"Round Number: {rounds}");
            return rounds;
        }
    }

    public int SetCurrentRound
    {
        set
        {
            rounds = value;
        }
    }
    public int IncrementRound { set => rounds += 1; }
    public int GetLives { get => lives; }
    public int SetLives { get => lives; set => lives = value; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        minigamePrefabs = Resources.LoadAll<GameObject>(MINIGAME_GAMEOBJECT_PATHS);
        MinigameBag.numberOfMinigames = minigamePrefabs.Length;
    }

    private void LateUpdate()
    {
        Time.timeScale = Aceleration.Scale;
    }

    public Minigame.Minigame LoadMinigame()
    {
        int minigameID = MinigameBag.GetRandomMinigame();

        Vector3 InitialMinigamePosition = Vector3.up * -20;

        GameObject minigameObject = Instantiate(minigamePrefabs[minigameID], InitialMinigamePosition, Quaternion.identity);

        return Minigame.Minigame.of(minigameID, minigameObject);
    }

    public void DestroyGameObject(GameObject obj, float delay)
    {
        Destroy(obj, delay);
    }
}