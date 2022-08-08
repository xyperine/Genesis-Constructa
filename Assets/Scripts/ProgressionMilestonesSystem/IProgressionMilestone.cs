using System;

namespace ColonizationMobileGame.ProgressionTracking
{
    public interface IProgressionMilestone
    {
        public event Action<ProgressionMilestoneType> Achieved;
    }
}