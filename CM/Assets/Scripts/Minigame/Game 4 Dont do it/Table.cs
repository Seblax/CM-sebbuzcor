using Animation;
using UnityEngine;

namespace Minigame.Game4
{
    public class Table : MonoBehaviour
    {
        Hop hop;
        Shake shake;
        PlayerInputDetector playerInputDetector;

        private void Start()
        {
            MinigameManager.instance.minigame.MinigameScoreValue = (int)((MinigameManager.instance.minigame.minigameTimer * Data.Minigame.Game2.RATIO_SCORE_POINTS_TIMER + Data.Minigame.Game2.BASE_SCORE));

            hop = GetComponent<Hop>();
            shake = GetComponent<Shake>();

            playerInputDetector = GetComponentInParent<PlayerInputDetector>();
            playerInputDetector.PlayerInputDetected += TableAnimation;
        }

        private void Update()
        {
            if (MinigameManager.instance.minigame.TimerPercent <= 0)
            {
                Destroy(gameObject, 1f);
            }
        }

        void TableAnimation()
        {
            MinigameManager.instance.minigame.MinigameScoreValue = (int)((Data.Minigame.Game4.BASE_SCORE));

            playerInputDetector.PlayerInputDetected -= TableAnimation;

            hop.Play(Data.Minigame.Game4.Table.HOP_SPEED, Data.Minigame.Game4.Table.AMPLITUDE);
            shake.PlayDelay(Data.Minigame.Game4.Table.SHAKE_SPEED, Data.Minigame.Game4.Table.INTERVAL, Data.Minigame.Game4.Table.DURATION, Data.Minigame.Game4.Table.DELAY);
            AudioManager.instance.PlayEffect(Data.Minigame.Game4.Table.TABLE_HIT_SOUND);
            AudioManager.instance.PlayEffect(Data.Minigame.Game4.Table.BAD_SURPRISE_SOUND);
        }
    }
}