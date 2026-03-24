using UnityEngine;
using gamemanager;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float minigameDuration = 7f;
    public float aceleration = 1f;

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
        Aceleration.SetScale = aceleration;
    }


}
