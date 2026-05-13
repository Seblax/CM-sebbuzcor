using StateManagement;

namespace Minigame.Game3
{
    public class DisturbCatState : IState
    {
        Cat cat;
        public DisturbCatState(Cat cat)
        {
            this.cat = cat;
        }

        public void OnEnter()
        {
            AudioManager.instance.PlayEffect(Data.Minigame.Game3.Cat.CAT_VISIBLE_SOUND);
        }

        public void OnExecute()
        {
            if(!cat.Shaking) cat.Shake();
            cat.Timer();
        }

        public void OnExit()
        {

        }
    }
}