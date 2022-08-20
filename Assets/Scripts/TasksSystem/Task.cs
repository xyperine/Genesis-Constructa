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

        private ScoreCounter _scoreCounter;
        
        
        public ITaskRequirement Requirement => requirement;


        public void Setup(DataForTasks data, ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
            
            requirement.Setup(data);
            requirement.Fulfilled += Reward;
        }


        private void Reward()
        {
            Debug.Log(actionToDo + " complete!");
            _scoreCounter.Add(reward);
        }
    }
}