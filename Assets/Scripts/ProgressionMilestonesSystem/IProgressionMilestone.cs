using System;

namespace GenesisConstructa.ProgressionMilestonesSystem
{
    public interface IProgressionMilestone
    {
        public event Action<ProgressionMilestoneType> Achieved;
    }
}