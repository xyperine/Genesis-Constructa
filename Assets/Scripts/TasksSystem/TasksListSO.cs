using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ColonizationMobileGame.TasksSystem
{
    [CreateAssetMenu(fileName = "Tasks_List_SO", menuName = "Tasks List", order = 0)]
    public class TasksListSO : SerializedScriptableObject
    {
        [OdinSerialize] private List<Task> mainTasks = new List<Task>();
        [OdinSerialize] private List<Task> optionalTasks = new List<Task>();

        public List<Task> MainTasks => mainTasks;
        public List<Task> OptionalTasks => optionalTasks;
        public List<Task> AllTasks => mainTasks.Concat(optionalTasks).ToList();
    }
}