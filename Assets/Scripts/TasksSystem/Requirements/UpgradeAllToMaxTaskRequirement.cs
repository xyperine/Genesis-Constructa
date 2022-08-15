using System;
using System.Linq;

namespace ColonizationMobileGame.TasksSystem.Requirements
{
    [Serializable]
    public class UpgradeAllToMaxTaskRequirement : TaskRequirement
    {
        public override Progress Progress => new Progress(
            data.LevelData.Structures.Count(s => s.Level == s.MaxLevel),
            data.LevelData.Structures.Count);

        
        protected override void Subscribe()
        {
            data.LevelData.Changed += OnDataChanged;
        }


        protected override void Unsubscribe()
        {
            data.LevelData.Changed -= OnDataChanged;
        }
    }
}