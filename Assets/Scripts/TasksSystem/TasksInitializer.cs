using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.Level;
using ColonizationMobileGame.ScoreSystem;
using UnityEngine;

namespace ColonizationMobileGame.TasksSystem
{
    public class TasksInitializer : MonoBehaviour, ILevelDataUser
    {
        [SerializeField] private TasksListSO tasksListSO;

        private LevelData _levelData;
        private ScoreModifier _scoreModifier;
        
        private List<Task> _tasks;


        public void SetLevelData(LevelData levelData)
        {
            _levelData = levelData;
        }


        public void SetScoreCounter(ScoreModifier scoreModifier)
        {
            _scoreModifier = scoreModifier;
        }


        public void InitializeTasks()
        {
            if (!tasksListSO)
            {
                return;
            }
            
            _tasks = tasksListSO.AllTasks;

            DataForTasks data = new DataForTasks(_levelData, _tasks.Select(t => t.Requirement).ToArray());
            foreach (Task task in _tasks)
            {
                task.Setup(data, _scoreModifier);
            }
        }
    }
}