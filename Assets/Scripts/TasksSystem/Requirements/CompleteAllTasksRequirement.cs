using System;
using System.Linq;

namespace ColonizationMobileGame.TasksSystem.Requirements
{
    [Serializable]
    public class CompleteAllTasksRequirement : TaskRequirement
    {
        private ITaskRequirement[] _taskRequirements;

        public override Progress Progress =>
            new Progress(_taskRequirements.Count(c => c.IsFulfilled), _taskRequirements.Length);

        
        protected override void Subscribe()
        {
            _taskRequirements = data.TaskRequirements.Where(r => r is not CompleteAllTasksRequirement).ToArray();
            
            foreach (ITaskRequirement taskRequirement in _taskRequirements)
            {
                taskRequirement.Fulfilled += OnDataChanged;
            }
        }


        protected override void Unsubscribe()
        {
            // Unsubscribe?
        }
    }
}