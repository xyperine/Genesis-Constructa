using System.Diagnostics.CodeAnalysis;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ColonizationMobileGame.TasksSystem
{
    [ShowOdinSerializedPropertiesInInspector]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Task
    {
        [OdinSerialize] private string title;
        [OdinSerialize] private float reward;
        [OdinSerialize] private ITaskRequirement requirement;
        
        public ITaskRequirement Requirement => requirement;


        public void Setup(DataForTasks data)
        {
            requirement.Setup(data);
            requirement.Fulfilled += () => Debug.Log(title + " complete!");
        }
    }
}