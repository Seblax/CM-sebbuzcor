using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateManagement
{
    public interface IState
    {
        public void OnEnter();
        public void OnExecute();
        public void OnExit();
    }
}
