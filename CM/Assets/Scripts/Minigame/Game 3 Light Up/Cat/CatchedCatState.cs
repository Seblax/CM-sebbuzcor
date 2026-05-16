using StateManagement;

namespace Minigame.Game3
{
    public class CatchedCatState : IState
    {
        Cat cat;
        public CatchedCatState(Cat cat) { 
            this.cat = cat;
        }

        public void OnEnter()
        {
            cat.Catched();
            MinigameManager.instance.minigame.MinigameScoreValue = (int)((MinigameManager.instance.minigame.minigameTimer * Data.Minigame.Game3.RATIO_SCORE_POINTS_TIMER + Data.Minigame.Game3.BASE_SCORE));
        }

        public void OnExecute()
        {
            cat.CatchedAnimation();
        }

        public void OnExit()
        {
        }
    }
}