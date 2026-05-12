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