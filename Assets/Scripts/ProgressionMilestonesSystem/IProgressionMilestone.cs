using System;

namespace ColonizationMobileGame.ProgressionMilestonesSystem
{
    public interface IProgressionMilestone
    {
        public event Action<ProgressionMilestoneType> Achieved;
    }
}