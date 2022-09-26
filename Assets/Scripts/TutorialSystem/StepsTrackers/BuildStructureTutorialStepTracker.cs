using ColonizationMobileGame.BuildSystem;
using UnityEngine;

namespace ColonizationMobileGame.TutorialSystem.StepsTrackers
{
    public class BuildStructureTutorialStepTracker : TutorialStepTracker
    {
        [SerializeField] private Builder structureBuilder;


        private void OnEnable()
        {
            structureBuilder.Built += InvokeCompleted;
        }


        private void OnDisable()
        {
            structureBuilder.Built -= InvokeCompleted;
        }
    }
}