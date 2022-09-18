using System;

namespace ColonizationMobileGame.UI.ArrowPointers.Target
{
    public abstract class ArrowPointerTargetInvalidationCondition : IDisposable
    {
        public bool Met { get; private set; }

        public event Action Satisfied;
        

        public abstract void StartTracking(ArrowPointerTarget target);


        protected void InvokeSatisfied()
        {
            Met = true;
            Satisfied?.Invoke();
        }


        public abstract void Dispose();
    }
}