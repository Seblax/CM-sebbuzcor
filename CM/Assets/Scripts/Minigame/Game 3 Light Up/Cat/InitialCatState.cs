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
            cat.SetRandomPosition();
        }

        public void OnExecute()
        {

        }

        public void OnExit()
        {
            
        }
    }

}