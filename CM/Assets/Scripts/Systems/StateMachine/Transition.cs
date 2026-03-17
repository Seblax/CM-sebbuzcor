using System;

namespace StateManagement
{
    public struct Transition
    {
        public Func<bool> Condition;
        public IState Source;
        public IState Target;
    }
}