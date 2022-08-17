using ColonizationMobileGame.Level;

namespace ColonizationMobileGame.TasksSystem
{
    public class DataForTasks
    {
        public LevelData LevelData { get; }
        public ITaskRequirement[] TaskRequirements { get; }


        public DataForTasks(LevelData levelData, ITaskRequirement[] taskRequirements)
        {
            LevelData = levelData;
            TaskRequirements = taskRequirements;
        }
    }
}