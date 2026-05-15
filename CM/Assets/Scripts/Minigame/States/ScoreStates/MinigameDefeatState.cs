
namespace Minigame
{
    public class MinigameDefeatState : MinigameScoreState
    {
        public MinigameDefeatState(Minigame minigame) : base(minigame)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();

            Lives -= 1;

            AudioManager.instance.PlayEffect("LoseGame");
        }

        public override void OnExecute()
        {
            base.OnExecute();
            if (timer < 1.5f)
            {
                ui.UpdateScoreUI(true);
            }
        }
    }
}
