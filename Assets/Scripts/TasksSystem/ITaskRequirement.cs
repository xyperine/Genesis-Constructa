using System;

namespace ColonizationMobileGame.TasksSystem
{
    public interface ITaskRequirement
    {
        bool IsFulfilled { get; }
        Progress Progress { get; }

        event Action Fulfilled;

        void Setup(DataForTasks dataForTasks);
    }
}