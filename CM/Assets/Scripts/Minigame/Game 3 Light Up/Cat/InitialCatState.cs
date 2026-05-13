using StateManagement;

namespace Minigame.Game3
{
    public class InitialCatState : IState
    {
        private Cat cat;
        public InitialCatState(Cat cat)
        {
            this.cat = cat;
        }

        public void OnEnter()
        {

        }

        public void OnExecute()
        {

        }

        public void OnExit()
        {
            AudioManager.instance.PlayEffect(Data.Minigame.Game3.GRAVEYARD_MUSIC_SOUND);
            cat.SetPosition();
        }
    }

}