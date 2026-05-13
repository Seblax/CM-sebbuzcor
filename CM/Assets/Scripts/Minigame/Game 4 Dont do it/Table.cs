using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using Unity.VisualScripting;
using UnityEngine;

namespace Minigame.Game4
{
    public class Table : MonoBehaviour
    {
        Hop hop;
        PlayerInputDetector playerInputDetector;

        private void Start()
        {
            hop = GetComponent<Hop>();

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
            hop.Play(Data.Minigame.Game4.Table.SPEED, Data.Minigame.Game4.Table.AMPLITUDE);
        }
    }
}