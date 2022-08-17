﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.TasksSystem
{
    public class TasksBuilder : MonoBehaviour, ILevelDataUser
    {
        [SerializeField] private TasksListSO tasksListSO;

        private LevelData _levelData;
        
        private List<Task> _tasks;


        public void SetLevelData(LevelData levelData)
        {
            _levelData = levelData;
        }


        private void Awake()
        {
            _levelData = FindObjectOfType<LevelData>();
            _tasks = tasksListSO.AllTasks;

            DataForTasks data = new DataForTasks(_levelData, _tasks.Select(t => t.Requirement).ToArray());
            foreach (Task task in _tasks)
            {
                task.Setup(data);
            }
        }
    }
}