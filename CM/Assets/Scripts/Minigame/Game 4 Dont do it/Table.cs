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
            playerInputDetector.PlayerInputDetected -= TableAnimation;

            hop.Play(Data.Minigame.Game4.Table.HOP_SPEED, Data.Minigame.Game4.Table.AMPLITUDE);
            shake.PlayDelay(Data.Minigame.Game4.Table.SHAKE_SPEED, Data.Minigame.Game4.Table.INTERVAL, Data.Minigame.Game4.Table.DURATION, Data.Minigame.Game4.Table.DELAY);
            AudioManager.instance.PlayEffect(Data.Minigame.Game4.Table.TABLE_HIT_SOUND);
            AudioManager.instance.PlayEffect(Data.Minigame.Game4.Table.BAD_SURPRISE_SOUND);
        }
    }
}