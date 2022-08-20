using System;
using ColonizationMobileGame.ScoreSystem;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ColonizationMobileGame.TasksSystem
{
    [Serializable]
    public class Task
    {
        [SerializeField] private string actionToDo;
        [SerializeField, MinValue(0)] private int reward;
        // ReSharper disable once InconsistentNaming
        [OdinSerialize] private ITaskRequirement requirement;

        private ScoreModifier _scoreModifier;
        
        
        public ITaskRequirement Requirement => requirement;


        public void Setup(DataForTasks data, ScoreModifier scoreModifier)
        {
            _scoreModifier = scoreModifier;
            
            requirement.Setup(data);
            requirement.Fulfilled += Reward;
        }


        private void Reward()
        {
            Debug.Log(actionToDo + " complete!");
            _scoreModifier.Add(reward);
        }
    }
}