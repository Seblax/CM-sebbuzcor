using StateManagement;
using UnityEngine;

namespace ui
{
    public class EndState : MonoBehaviour, IState
    {
        Transition transition;
        public EndState(Transition transition) { 
            this.transition = transition;
        }
        public void OnEnter()
        {
            Destroy(this.transition.gameObject);
        }

        public void OnExecute()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }

}