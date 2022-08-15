using System;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.TasksSystem.Requirements
{
    [Serializable]
    public class BuildTaskRequirement : TaskRequirement
    {
        [SerializeField] private StructureType structureType;
        [SerializeField] private int structuresRequired = 1;

        public override Progress Progress =>
            new Progress(data.LevelData.Structures.Count(s => s.Type == structureType),
                structuresRequired);

        
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