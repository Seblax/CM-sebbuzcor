using StateManagement;

namespace Minigame.Game3
{
    public class CatchedCatState : IState
    {
        Cat _cat;
        bool _isPaused;

        public CatchedCatState(Cat cat, bool isPaused) { 
            this._cat = cat;
            this._isPaused = isPaused;
        }

        public void OnEnter()
        {
            if (_isPaused) return;

            _cat.Catched();
            MinigameManager.instance.minigame.MinigameScoreValue = (int)((MinigameManager.instance.minigame.minigameTimer * Data.Minigame.Game3.RATIO_SCORE_POINTS_TIMER + Data.Minigame.Game3.BASE_SCORE));
        }

        public void OnExecute()
        {
            if (_isPaused) return;
            _cat.CatchedAnimation();
        }

        public void OnExit()
        {

        }
    }
}