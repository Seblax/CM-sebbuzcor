
using Gamemanager;
using UnityEngine;

namespace Minigame
{
    public class MinigameVictoryState : MinigameScoreState
    {
        public MinigameVictoryState(Minigame minigame) : base(minigame)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
            AudioManager.instance.PlayEffect("WinGame");
        }
    }
}
