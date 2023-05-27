using GenesisConstructa.BuildSystem;
using UnityEngine;

namespace GenesisConstructa.ProgressionMilestonesSystem
{
    public class MetalFactoryBuiltMilestone : ProgressionMilestone
    {
        [SerializeField] private Builder builder;
        
        protected override ProgressionMilestoneType Type => ProgressionMilestoneType.MetalFactoryBuilt;


        protected override void Subscribe()
        {
            builder.Built += InvokeAchieved;
        }


        protected override void Unsubscribe()
        {
            builder.Built -= InvokeAchieved;
        }
    }
}