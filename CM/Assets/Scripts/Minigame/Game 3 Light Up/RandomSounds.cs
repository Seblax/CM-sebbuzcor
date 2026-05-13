using Minigame;
using UnityEngine;

public class RandomSounds : MonoBehaviour, IPausable
{
    [SerializeField] private SoundEffect soundEffect;
    private float timer;
    private bool paused;
    public bool IsPaused => paused;


    private void Start()
    {
        timer = Random.Range(0, Data.Minigame.Game3.RANDOM_SOUND_TIMER);
    }

    private void OnEnable()
    {
        if (MinigameManager.instance != null)
            MinigameManager.instance.Pause += SetPaused;
    }

    private void OnDisable()
    {
        if (MinigameManager.instance != null)
            MinigameManager.instance.Pause -= SetPaused;
    }

    void Update()
    {
        if (IsPaused || MinigameManager.instance.minigame.TimerPercent < 0.25f) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = Random.Range(0, Data.Minigame.Game3.RANDOM_SOUND_TIMER);
            AudioManager.instance.PlayEffect(soundEffect);
        }
    }

    public void SetPaused(bool isPaused)
    {
        this.paused = isPaused;
    }
}
