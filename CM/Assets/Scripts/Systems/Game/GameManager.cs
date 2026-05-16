using Gamemanager;
using Minigame;
using Score;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public readonly string MINIGAME_GAMEOBJECT_PATHS = "Prefabs/Minigames/";
    public GameObject[] minigamePrefabs;

    public float minigameDuration = 7f;

    public float minigameTimer;
    public int lives = 3;
    private int score = 0;

    public int rounds = 0;
    public int roundsIncrementation = 3;

    public bool isGameOver;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public int GetCurrentRound
    {
        get
        {
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

    public bool IsStillAlive
    {
        get
        {
            return GetLives > 0;
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

        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name != "Minigame")
        {
            ResetGameManager();
        }
    }

    public void ResetGameManager()
    {
        int score = this.score;

        Aceleration.Reset();

        ScoreDataService.SaveScore();

        Destroy(this.gameObject);
    }

    public Minigame.Minigame LoadMinigame()
    {
        int minigameID = MinigameBag.GetRandomMinigame();

        Vector3 InitialMinigamePosition = Vector3.up * -20;

        GameObject minigameObject = Instantiate(minigamePrefabs[minigameID], InitialMinigamePosition, Quaternion.identity);

        Debug.LogWarning($"Minigame {minigameID} loaded");

        return Minigame.Minigame.of(minigameID, minigameObject);
    }

    public void DestroyGameObject(GameObject obj, float delay)
    {
        Destroy(obj, delay);
    }

    public void Reset()
    {
        minigameDuration = 7f;

        lives = 3;
        score = 0;

        rounds = 0;
        roundsIncrementation = 3;

        isGameOver = false;
    }

    public void SaveScore() {
        Score += (int)(MinigameManager.instance.minigame.MinigameScoreValue * Aceleration.Scale);
        PlayerPrefs.SetInt("HotSave", Score);
    }
}