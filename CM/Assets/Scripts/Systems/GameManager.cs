using UnityEngine;
using Gamemanager;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float minigameDuration = 7f;

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
