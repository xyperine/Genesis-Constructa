using System;

namespace ColonizationMobileGame.TasksSystem
{
    public abstract class TaskRequirement : ITaskRequirement
    {
        protected DataForTasks data;

        public bool IsFulfilled { get; private set; }
        public abstract Progress Progress { get; }

        public event Action Fulfilled;
        
        
        public void Setup(DataForTasks dataForTasks)
        {
            data = dataForTasks;
            IsFulfilled = false;
            
            Subscribe();
        }


        protected abstract void Subscribe();


        protected void OnDataChanged()
        {
            if (!Progress.Complete)
            {
                return;
            }
            
            Unsubscribe();
            
            IsFulfilled = true;
            Fulfilled?.Invoke();
        }


        protected abstract void Unsubscribe();
    }
}