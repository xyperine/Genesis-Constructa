using GenesisConstructa.Structures;
using GenesisConstructa.UnlockingSystem;
using UnityEngine;

namespace GenesisConstructa.TutorialSystem.StepsTrackers
{
    public class UnlockSomethingTutorialStepTracker : TutorialStepTracker
    {
        [SerializeField] private StructureType structureToUnlock;
        [SerializeField] private UnlockStation unlockStation;
        
        
        private void OnEnable()
        {
            unlockStation.Unlocked += OnUnlocked;
        }


        private void OnDisable()
        {
            unlockStation.Unlocked -= OnUnlocked;
        }


        private void OnUnlocked(StructureIdentifier identifier)
        {
            if (identifier.StructureType == structureToUnlock)
            {
                InvokeCompleted();
            }
        }
    }
}