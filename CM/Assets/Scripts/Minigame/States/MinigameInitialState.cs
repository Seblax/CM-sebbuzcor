namespace Minigame
{
    public class MinigameInitialState : MinigameState
    {
        public MinigameInitialState(Minigame minigame) : base(minigame) { 
        
        }

        public override void OnEnter()
        {
            base.OnEnter();
            SetOff();
            mover.OnStartMove(() => SetActiveOn(ui.tittle));
            mover.OnStartMove(() => ui.UpdateTittleUI(ui.GetTittle(minigame.ID)));
        }

        public override void OnExecute()
        {
            if (moveRequested) return;

            if (!manager.isMoving)
            {
                moveRequested = true;
                mover.Play();
            }
        }

        void SetOff() {
            SetActiveOff(ui.tittle);
            SetActiveOff(ui.score);
            SetActiveOff(ui.minigame);
        }
    }
}
