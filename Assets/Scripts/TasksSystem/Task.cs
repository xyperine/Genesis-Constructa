using System;
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
        
        public ITaskRequirement Requirement => requirement;


        public void Setup(DataForTasks data)
        {
            requirement.Setup(data);
            requirement.Fulfilled += () => Debug.Log(actionToDo + " complete!");
        }
    }
}